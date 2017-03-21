using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryRepresentation;

namespace TestFunctions
{
    public class Rosenbrock : ITestFunction
    {
        private double _lowerLimit = -2.048;
        private double _upperLimit = 2.048;
        private double E = 0.001;
        private int _numberOfBits;
        //number of bits.
        private int _n;
        public List<BinaryRepresentationOfFloat> Values { get; set; } = new List<BinaryRepresentationOfFloat>();
        private int _d;

        public Rosenbrock(int numberOfBits, int dimensions)
        {
            _d = dimensions;
            _numberOfBits = numberOfBits;
            _n = (int)Math.Pow(numberOfBits, 2);
            for (int i = 0; i < dimensions; i++)
            {
                Values.Add(new BinaryRepresentationOfFloat(numberOfBits, _lowerLimit, _upperLimit));
            }

        }
        public Rosenbrock(double lower, double upper, int numberOfBits, int dimensions)
        {
            _numberOfBits = numberOfBits;
            _d = dimensions;
            _lowerLimit = lower;
            _upperLimit = upper;
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
            //f2(x) = sum(100·(x(i + 1) - x(i) ^ 2) ^ 2 + (1 - x(i)) ^ 2)
            //we can say that x(i) is our current _number
            //what about x(i+1) -> compute the next number

            double sum = 0;
            for(int i =0; i< Values.Count - 1; i++)
            {
                double number = Values[i].ConvertToDouble();
                double nextNumber = Values[i+1].ConvertToDouble(1);

                sum += (100 * Math.Pow(nextNumber - Math.Pow(number, 2), 2))
              + Math.Pow(1 - number, 2);
            }
            return 1 / (sum + E);
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
    }
}
