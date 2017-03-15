using BinaryRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFunctions;

namespace MOC_Tema1
{
  public class HillClimbing
  {
    private BinaryRepresentationOfFloat MaxCurrentValue;
    private double BestValue;
    private ITestFunction function;
    public HillClimbing(ITestFunction function)
    {
      //get starting number:
      MaxCurrentValue = function.GetBinaryNumber();
      BestValue = function.getValueOfFitnessFunction();
      this.function = function;

    }

    public double GetBestValue()
    {
      bool noMoreSolutions = false;
      double currentBestScoreValue = BestValue;
      BinaryRepresentationOfFloat currentBestValue = MaxCurrentValue;

      while (!noMoreSolutions)
      {
        noMoreSolutions = true;
        //construct neighbourhood
        var neighbourhood = currentBestValue.GetNeighbourhood();
        //get best solution of neightbourhood
       
        foreach (var neighbour in neighbourhood)
        {
          function.SetBinaryNumber(neighbour);
          var fitnessOfCurrent = function.getValueOfFitnessFunction();
          if (fitnessOfCurrent > currentBestScoreValue)
          {
            //new best
            currentBestValue = neighbour;
            currentBestScoreValue = fitnessOfCurrent;
            //continue search
            noMoreSolutions = false;
          }
        }
      }

      return currentBestValue.ConvertToDouble();
    }
  }
}
