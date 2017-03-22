using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryRepresentation;

namespace TestFunctions
{

    public class Griewangk : ITestFunction
    {
        private double _lowerLimit = -600;
        private double _upperLimit = 600;
        private double E = 0.001;
        private int _numberOfBits;
        private int _d;
        private int _n;
        public List<BinaryRepresentationOfFloat> Values { get; set; } = new List<BinaryRepresentationOfFloat>();

        public Griewangk(double lower, double upper, int numberOfBits, int dimensions)
        {
            _d = dimensions;
            _lowerLimit = lower;
            _upperLimit = upper;
            _numberOfBits = numberOfBits;
            for (int i = 0; i < dimensions; i++)
            {
                Values.Add(new BinaryRepresentationOfFloat(numberOfBits, _lowerLimit, _upperLimit));
            }
        }
        public Griewangk(int numberOfBits, int dimensions)
        {
            _d = dimensions;
            _n = (int)Math.Pow(numberOfBits, 2);
            _numberOfBits = numberOfBits;
            for (int i = 0; i < dimensions; i++)
            {
                Values.Add(new BinaryRepresentationOfFloat(numberOfBits, _lowerLimit, _upperLimit));
            }
        }
        public List<BinaryRepresentationOfFloat> GetBinaryNumber()
        {
            return Values;
        }

        public double getValueOfFitnessFunction()
        {
            //f8(x) = sum(x(i) ^ 2 / 4000) - prod(cos(x(i) / sqrt(i))) + 1, i = 1:n
            //we can say that x(i) is our current _number
            double sum = 0;
            double prod = 1;
            for(int i = 0; i< Values.Count; i++)
            {
                double number = Values[i].ConvertToDouble();
                sum += (Math.Pow(number, 2) / 4000);
                prod *= Math.Cos(number / Math.Sqrt(i + 1)); //i starts from value 0
            }


            var result = sum - prod + 1;
            return 1 / (result + E);
        }

        public void SetBinaryNumber(BinaryRepresentationOfFloat number, int index)
        {
            Values[index] = number;
        }

        public int GetNumberOfBits()
        {
            return _numberOfBits;
        }

        public Tuple<double, double> GetLowerUpperBounds()
        {
            return new Tuple<double, double>(_lowerLimit, _upperLimit);
        }

        public int getDimensions()
        {
            return _d;
        }
    }
}
