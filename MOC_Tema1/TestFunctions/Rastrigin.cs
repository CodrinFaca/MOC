using BinaryRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFunctions
{
  public class Rastrigin : ITestFunction
  {
    private double _lowerLimit = -5.12;
    private double _upperLimit = 5.12;
    private double _A = 10;
    private double E = 0.001;
    private int _numberOfBits;
    public BinaryRepresentationOfFloat _number { get; set; }
    //number of bits.
    private int _n;
    public Rastrigin(int numberOfBits)
    {
      _numberOfBits = numberOfBits;
      _n = (int)Math.Pow(numberOfBits, 2);
      _number = new BinaryRepresentationOfFloat(numberOfBits, _lowerLimit, _upperLimit);
    }
    public Rastrigin(double lower, double upper, int numberOfBits)
    {
      _numberOfBits = numberOfBits;
      _lowerLimit = lower;
      _upperLimit = upper;
      _number = new BinaryRepresentationOfFloat(numberOfBits, _lowerLimit, _upperLimit);
    }
    public double getValueOfFitnessFunction()
    {
      double result = _A * _n;
      //f6(x) = 10·n + sum(x(i) ^ 2 - 10·cos(2·pi·x(i))), i = 1:n; -5.12 <= x(i) <= 5.12.
      //we can say that x(i) is our current _number
      double number = _number.ConvertToDouble();
      var sum = Math.Pow(number, 2) - (10 * Math.Cos(2 * Math.PI * number));
      return 1/ (sum + result + E);
    }

    public BinaryRepresentationOfFloat GetBinaryNumber()
    {
      return _number;
    }

    public void SetBinaryNumber(BinaryRepresentationOfFloat number)
    {
      _number = number;
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
