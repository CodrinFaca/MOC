using BinaryRepresentation;
using System;
using System.Collections.Generic;

namespace TestFunctions
{

    public class Rastrigin : ITestFunction
    {
        private double _lowerLimit = -5.12;
        private double _upperLimit = 5.12;
        private int _A = 10;
        private double E = 0.001;
        private int _numberOfBits;
        //public BinaryRepresentationOfFloat _number { get; set; }
        public List<BinaryRepresentationOfFloat> Values { get; set; } = new List<BinaryRepresentationOfFloat>();
        //number of bits.
        private int _n;
        public Rastrigin(int numberOfBits, int dimensions)
        {
            _A = dimensions;
            _numberOfBits = numberOfBits;
            _n = (int)Math.Pow(numberOfBits, 2);
            //initialize values
            for(int i = 0; i< dimensions; i++)
            {
                Values.Add(new BinaryRepresentationOfFloat(numberOfBits, _lowerLimit, _upperLimit));
            }
        }
        public Rastrigin(double lower, double upper, int numberOfBits, int dimensions)
        {
            _numberOfBits = numberOfBits;
            _lowerLimit = lower;
            _upperLimit = upper;
            _A = dimensions;
            for (int i = 0; i < dimensions; i++)
            {
                Values.Add(new BinaryRepresentationOfFloat(numberOfBits, _lowerLimit, _upperLimit));
            }
        }
        public double getValueOfFitnessFunction()
        {
            double result = _A * Values.Count;
            //f6(x) = 10·n + sum(x(i) ^ 2 - 10·cos(2·pi·x(i))), i = 1:n; -5.12 <= x(i) <= 5.12.
            //we can say that x(i) is our current _number
            double sum = 0;
            foreach(var element in Values)
            {
                double number = element.ConvertToDouble();
                sum += Math.Pow(number, 2) - (10 * Math.Cos(2 * Math.PI * number));
            }
            return 1 / (sum + result + E);
        }

        public List<BinaryRepresentationOfFloat> GetBinaryNumber()
        {
            return Values;
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
            return _A;
        }
    }
}
