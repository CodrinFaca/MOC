using BinaryRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFunctions;

namespace MOC_Tema1
{
    class Program
    {
        static void Main(string[] args)
        {

            Rastrigin rastr = new Rastrigin(7, 10);
            Griewangk grn = new Griewangk(7, 10);
            Rosenbrock rosen = new Rosenbrock(7,10);

            //HillClimbing hc = new HillClimbing(rosen);
            //var bestSoFar = hc.GetBestValue();
            //string output = "";
            //foreach(var item in bestSoFar)
            //{
            //    output += $"{item} ";
            //}
            //Console.WriteLine(output);
            //Console.ReadKey();

            //GA:
            GeneticAlgorithm ga = new GeneticAlgorithm(rastr, 30);
            ga.SetParameters(100, 0.05, 0.7);
            var result = ga.Run();
            string outputGA = "";
            foreach (var item in result)
            {
                outputGA += $"{item.ConvertToDouble()} ";
            }
            Console.WriteLine($"Genetic algorithm result: {outputGA}");
            Console.ReadKey();
        }
    }
}
