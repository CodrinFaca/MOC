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

            Rastrigin rastr = new Rastrigin(5);
            Griewangk grn = new Griewangk(7);
            Rosenbrock rosen = new Rosenbrock(5);

            //HillClimbing hc = new HillClimbing(rastr);
            //var bestSoFar = hc.GetBestValue();
            //Console.WriteLine(bestSoFar);
            //Console.ReadKey();

        //GA:
            GeneticAlgorithm ga = new GeneticAlgorithm(rastr, 30);
            ga.SetParameters(500, 0.05, 0.7);
            var result = ga.Run();

            Console.WriteLine($"Genetic algorithm result: {result.ConvertToDouble()}");
            Console.ReadKey();
        }
    }
}
