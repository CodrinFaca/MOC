using BinaryRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFunctions;

namespace MOC_Tema1
{
    public class GeneticAlgorithm
    {
        private ITestFunction _testFunction;
        private int _populationSize;
        private List<BinaryRepresentationOfFloat> _populationList;
        private List<BinaryRepresentationOfFloat> _finalSet = new List<BinaryRepresentationOfFloat>();
        private List<double> _scoreList;
        private double _totalFitness = 0;
        private List<double> _survivalProbability = new List<double>();
        private Random rng = new Random();
        //parameters
        private double _mutationRate = 0.05;
        private double _crossOverRate = 0.8;
        private double _crossOverPoint = 0.5;
        private int _numberOfSteps = 20;
        private int numberOfBits;
        public GeneticAlgorithm(ITestFunction testFunct, int popSize)
        {
            _testFunction = testFunct;
            numberOfBits = _testFunction.GetNumberOfBits();

            _populationSize = popSize;
            _scoreList = new List<double>(popSize);
            _populationList = new List<BinaryRepresentationOfFloat>(popSize);
        }

        public void SetParameters(int numberOfSteps, double mutationRate, double crossoverRate)
        {
            _mutationRate = mutationRate;
            _crossOverRate = crossoverRate;
        }
        public List<BinaryRepresentationOfFloat> Run()
        {
            //step 0: initialize size (dimensions)
            for (int item = 0; item < _testFunction.getDimensions(); item++)
            {
                //step 1: Initialization - create population
                Initialize();

                for (int i = 0; i < _numberOfSteps; i++)
                {
                    //step 2: evaluation
                    Evaluation(item, true);
                    //step 3: selection
                    //Console.WriteLine($"Step {i}: has total fitness = {_totalFitness}");
                    //Print(item);
                    var selectedIndividuals = Selection();
                    _populationList = selectedIndividuals;
                    //mutation
                    ApplyMutation();
                    //crossover
                    ApplyCrossover();
                    //Todo: add hybridisation at the end of this step.

                }
                //return best individual:

                Evaluation(item);
                BinaryRepresentationOfFloat bestMatch = GetBestMatch();
                _testFunction.SetBinaryNumber(bestMatch, item);
                _finalSet.Add(bestMatch);
                //return GetBestMatch();
            }
            return _finalSet;
        }

        private BinaryRepresentationOfFloat GetBestMatch()
        {
            var bestScore = _scoreList[0];
            var bestScoreIndex = 0;
            for (int i = 1; i < _populationSize; i++)
            {
                if (_scoreList[i] > bestScore)
                {
                    bestScore = _scoreList[i];
                    bestScoreIndex = i;
                }
            }
            return _populationList[bestScoreIndex];
        }

        private void ApplyCrossover()
        {
            var crossoverPosition = (int)(numberOfBits * _crossOverPoint);
            for (int i = 0; i < _populationSize - 2; i += 2)
            {
                if (rng.NextDouble() <= _crossOverRate)
                {
                    //Do crossover
                    var first = _populationList[i].BinaryRepresentation;
                    var second = _populationList[i + 1].BinaryRepresentation;
                    BinaryRepresentationOfFloat.CrossOver(crossoverPosition, ref first, ref second);

                }
            }
        }

        private void ApplyMutation()
        {
            foreach (var item in _populationList)
            {
                for (int i = 0; i < numberOfBits; i++)
                {
                    if (rng.NextDouble() < _mutationRate)
                    {
                        item.FlipBit(i);
                    }
                }
            }
        }

        private List<BinaryRepresentationOfFloat> Selection()
        {
            List<BinaryRepresentationOfFloat> selectedIndividuals = new List<BinaryRepresentationOfFloat>();
            for (int i = 0; i < _populationSize; i++)
            {
                //normalization of random number
                double luckyNumber7 = rng.NextDouble() * _totalFitness;
                int index = GetIndexOfSurvivalIndividual(luckyNumber7);
                var cpy = _populationList[index].Clone();
                selectedIndividuals.Add(cpy);

            }
            return selectedIndividuals;
        }

        private int GetIndexOfSurvivalIndividual(double luckyNumber7)
        {
            //divide et impera
            int startIndex = 0;
            int endIndex = _populationSize - 1;
            int result = 0;
            while (endIndex - startIndex > 1)
            {
                int mid = ((endIndex - startIndex) / 2) + startIndex;
                if (_scoreList[mid] > luckyNumber7)
                {
                    endIndex = mid;
                }
                else
                {
                    startIndex = mid;
                }
            }
            result = startIndex;
            return result;
        }

        private void Evaluation(int step, bool print = false)
        {
            int i = 0;
            double lastScore = 0;
            foreach (var item in _populationList)
            {
                //calculate fitness of item
                _testFunction.SetBinaryNumber(item, step);
                var fitness = _testFunction.getValueOfFitnessFunction();
                _scoreList[i] = fitness;
                _totalFitness += fitness;
                _survivalProbability.Add(_totalFitness);
                i++;
                if(fitness > lastScore)
                    lastScore = fitness;
            }
            if(print)
                Console.WriteLine($"best fitnes at each step: {1/lastScore}; i = {step}");
        }

        private void Initialize()
        {
            var lower_upper = _testFunction.GetLowerUpperBounds();
            for (int i = 0; i < _populationSize; i++)
            {
                //a new Binary representation of float is by default random
                var number = new BinaryRepresentationOfFloat(numberOfBits, lower_upper.Item1, lower_upper.Item2);
                _populationList.Add(number);
                //just fill list
                _scoreList.Add(0);
            }
        }
        private void Print(int step)
        {
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine($"cromosome{i} as function value = {1 / _scoreList[i]}.");
            }
            Console.WriteLine();
        }
    }
}
