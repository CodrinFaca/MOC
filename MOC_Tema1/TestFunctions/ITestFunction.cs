using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFunctions
{
    public interface ITestFunction
    {

    double getValueOfFitnessFunction();
    BinaryRepresentation.BinaryRepresentationOfFloat GetBinaryNumber();
    void SetBinaryNumber(BinaryRepresentation.BinaryRepresentationOfFloat number);

    int GetNumberOfBits();

    Tuple<double, double> GetLowerUpperBounds();
    }
}
