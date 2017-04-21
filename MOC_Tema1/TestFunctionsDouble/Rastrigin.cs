using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFunctionsDouble
{
  public class Rastrigin : ITestFunction
  {
    private readonly double LOWER_BOUND = 0;
    private readonly double UPPER_BOUND = 0;
    private readonly int _dimensions = 0;
   
    public Rastrigin(int dimensions)
    {
      _dimensions = dimensions;
    }
    public Rastrigin(int dimensions, double lower, double upper)
    {
      _dimensions = dimensions;
      LOWER_BOUND = lower;
      UPPER_BOUND = upper;
    }

    public double GetValueOfFunction()
    {
      throw new NotImplementedException();
    }

    public Tuple<double, double> GetLowerUpperBounds()
    {
      return new Tuple<double, double>(LOWER_BOUND, UPPER_BOUND);
    }

    public int GetDimension()
    {
      return _dimensions;
    }
  }
}
