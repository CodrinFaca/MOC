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
    public BinaryRepresentationOfFloat _number { get; set; }
    private int _n;

    public Griewangk(double lower, double upper, int numberOfBits)
    {
      _lowerLimit = lower;
      _upperLimit = upper;
      _numberOfBits = numberOfBits;
      _number = new BinaryRepresentationOfFloat(numberOfBits, _lowerLimit, _upperLimit);
    }
    public Griewangk(int numberOfBits)
    {
      _n = (int)Math.Pow(numberOfBits, 2);
      _numberOfBits = numberOfBits;
      _number = new BinaryRepresentationOfFloat(numberOfBits, _lowerLimit, _upperLimit);
    }
    public BinaryRepresentationOfFloat GetBinaryNumber()
    {
      return _number;
    }

    public double getValueOfFitnessFunction()
    {
      //f8(x) = sum(x(i) ^ 2 / 4000) - prod(cos(x(i) / sqrt(i))) + 1, i = 1:n
      //we can say that x(i) is our current _number
      // i = 1
      double number = _number.ConvertToDouble();

      var result = (Math.Pow(number, 2) / 4000) - Math.Cos(number) + 1;
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
