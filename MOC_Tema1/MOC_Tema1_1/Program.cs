using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOC_Tema1_1
{
    class Program
    {
        static void Main(string[] args)
        {

            HillClimbing hc = new HillClimbing();
            hc.Fill();
            hc.BestImprovement();
            hc.FirstImprovement();

            Console.ReadKey();
        }
    }
}
