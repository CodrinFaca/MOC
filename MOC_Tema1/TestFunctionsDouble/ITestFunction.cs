using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFunctionsDouble
{
    public interface ITestFunction
    {
      double GetValueOfFunction();
      Tuple<double, double> GetLowerUpperBounds();
      int GetDimension();
    }
}
