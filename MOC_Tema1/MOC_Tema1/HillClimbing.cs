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
        private List<BinaryRepresentationOfFloat> MaxCurrentValue;
        private double BestValue;
        private ITestFunction function;
        public HillClimbing(ITestFunction function)
        {
            //get starting number:
            MaxCurrentValue = function.GetBinaryNumber();
            BestValue = function.getValueOfFitnessFunction();
            this.function = function;

        }

        public List<double> GetBestValue()
        {
            List<BinaryRepresentationOfFloat> currentBestValue = MaxCurrentValue;
            //iterate through each value in array  and improve it as much as possible
            var index = 0;
            foreach (var item in function.GetBinaryNumber().ToList())
            {
                bool noMoreSolutions = false;

                BinaryRepresentationOfFloat currentBesetVal = null;
                double currentBestScoreValue = BestValue;

                while (!noMoreSolutions)
                {
                    noMoreSolutions = true;
                    //construct neighbourhood
                    var neighbourhood = item.GetNeighbourhood();
                    //get best solution of neightbourhood

                    foreach (var neighbour in neighbourhood)
                    {
                        function.SetBinaryNumber(neighbour, index);
                        var fitnessOfCurrent = function.getValueOfFitnessFunction();
                        if (fitnessOfCurrent > currentBestScoreValue)
                        {
                            //new best
                            currentBesetVal = neighbour;
                            currentBestScoreValue = fitnessOfCurrent;
                            //continue search
                            noMoreSolutions = false;
                        }
                    }
                }
                currentBestValue[index] = currentBesetVal;
                index++;
            }
            
            return currentBestValue.Select(x => x.ConvertToDouble()).ToList();
        }
    }
}
