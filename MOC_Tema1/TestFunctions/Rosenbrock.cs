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
    public BinaryRepresentationOfFloat _number { get; set; }
    //number of bits.
    private int _n;

    public Rosenbrock(int numberOfBits)
    {
      _numberOfBits = numberOfBits;
      _n = (int)Math.Pow(numberOfBits, 2);
      _number = new BinaryRepresentationOfFloat(numberOfBits, _lowerLimit, _upperLimit);
    }
    public Rosenbrock(double lower, double upper, int numberOfBits)
    {
      _numberOfBits = numberOfBits;
      _lowerLimit = lower;
      _upperLimit = upper;
      _number = new BinaryRepresentationOfFloat(numberOfBits, _lowerLimit, _upperLimit);
    }

    public BinaryRepresentationOfFloat GetBinaryNumber()
    {
      return _number;
    }

    public double getValueOfFitnessFunction()
    {
      //f2(x) = sum(100·(x(i + 1) - x(i) ^ 2) ^ 2 + (1 - x(i)) ^ 2)
      //we can say that x(i) is our current _number
      //what about x(i+1) -> compute the next number
      double number = _number.ConvertToDouble();
      double nextNumber = _number.ConvertToDouble(1);

      var result = (100 * Math.Pow(nextNumber - Math.Pow(number, 2), 2))
        + Math.Pow(1 - number, 2);

      return 1 / (result + E);
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
