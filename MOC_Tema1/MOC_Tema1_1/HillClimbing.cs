using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryRepresentation;

namespace MOC_Tema1_1
{
    public class HillClimbing
    {
        //bazinele de atractie
        private double[] firstImprovement = new double[32];
        private double[] bestImprovement = new double[32];
        private Dictionary<double, List<int>> bestImprovementDictionary = new Dictionary<double, List<int>>();
        private Dictionary<double, List<int>> firstImprovementDictionary = new Dictionary<double, List<int>>();
        private BinaryRepresentation[] numbers = new BinaryRepresentation[32];

        private double TestFunction(BinaryRepresentation number)
        {
            var x = number.ConvertToDouble();
            if (x > 31 || x < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            double sum = 100;
            sum += Math.Pow(x, 3);
            sum -= (60 * x * x);
            sum += 900 * x;

            return sum;
        }

        public void Fill()
        {
            for (int i = 0; i < 32; i++)
            {
               var arr = new BitArray(BitConverter.GetBytes(i));
               var b = new BinaryRepresentation(arr);
                numbers[i] = b;
            }
        }

        public void BestImprovement()
        {
            //initialize
            double bestScore;
            int bestIndex = -1;
            //iteration to pass through all numbers
            while (true)
            {
                List<int> samePool = new List<int>();
                int startIndexValue = ComputeFirstUncheckedNumber();
                if (startIndexValue == -1)
                {
                    break;
                }
                bool canClimb = true;
                bestScore = TestFunction(numbers[startIndexValue]);
                bestIndex = startIndexValue;
                //do Hillclimbing
                BinaryRepresentation currentNumber = numbers[startIndexValue];
                while (canClimb)
                {
                    List<BinaryRepresentation> neighbours = GetNeighbours(currentNumber);
                    //bestScore = TestFunction(numbers[startIndexValue]);
                    //bestIndex = startIndexValue;
                    samePool.Add(bestIndex);

                    canClimb = false;
                    foreach (var neighbour in neighbours)
                    {
                        var functionValue = TestFunction(neighbour);
                        if (functionValue > bestScore)
                        {
                            bestScore = functionValue;
                            bestIndex = (int)neighbour.ConvertToDouble();
                            canClimb = true;
                            currentNumber = neighbour;
                        }
                    }
                    //Console.WriteLine("improvement: " + bestIndex);
                }
                //mark all in the same pool
                foreach (var index in samePool)
                {
                    if (bestImprovement[index] <= bestScore)
                    {
                        bestImprovement[index] = bestScore;
                    }
                }
                if (!bestImprovementDictionary.ContainsKey(bestScore))
                {
                    bestImprovementDictionary.Add(bestScore, samePool);
                }
                else
                {
                    bestImprovementDictionary[bestScore].AddRange(samePool);
                }
                bestImprovementDictionary[bestScore].Insert(0,samePool.Last());

            }
            Print();
        }

        private void Print(bool isBestImprovement = true)
        {
            Console.WriteLine(isBestImprovement ? "Best improvement results" : "First improvement results");
            //var testArray = isBestImprovement ? bestImprovement : firstImprovement;
            //for (int i = 0; i < 32; i++)
            //{
            //    Console.WriteLine($"{i} has final value: {testArray[i]}");
            //}
            var testDictionary = isBestImprovement ? bestImprovementDictionary : firstImprovementDictionary;
            foreach (var kvp in testDictionary)
            {
                //var numbers = kvp.Value.Distinct().ToList();
                var numbersString = string.Join(",", kvp.Value.Distinct().Select(x => x.ToString()));
                Console.WriteLine($"Pool with max: {kvp.Key} contains the following numbers: {numbersString}");

            }
        }

        private List<BinaryRepresentation> GetNeighbours(BinaryRepresentation d)
        {
            List <BinaryRepresentation> result = new List<BinaryRepresentation>();
            for (int i = 0; i < 5; i++)
            {
                result.Add(d.GetNewByFlipping(i));
            }
            return result;
        }


        /// <summary>
        /// If paramenter is false, use First Improvement
        /// </summary>
        /// <param name="isBestImprovement"></param>
        /// <returns></returns>
        private int ComputeFirstUncheckedNumber(bool isBestImprovement = true)
        {
            var testArray = isBestImprovement ? bestImprovement : firstImprovement;
            for (int i = 0; i < 32; i++)
            {
                if (testArray[i] == 0)
                    return i;
            }
            return -1;
        }

        public void FirstImprovement()
        {
            //reset values 

            //initialize
            double bestScore;
            int bestIndex = -1;
            //iteration to pass through all numbers
            while (true)
            {
                List<int> samePool = new List<int>();
                int startIndexValue = ComputeFirstUncheckedNumber(false);
                if (startIndexValue == -1)
                {
                    break;
                }
                bool canClimb = true;
                bestScore = TestFunction(numbers[startIndexValue]);
                bestIndex = startIndexValue;
                //do Hillclimbing
                BinaryRepresentation currentNumber = numbers[startIndexValue];
                while (canClimb)
                {
                    List<BinaryRepresentation> neighbours = GetNeighbours(currentNumber);
                    //bestScore = TestFunction(numbers[startIndexValue]);
                    //bestIndex = startIndexValue;
                    samePool.Add(bestIndex);

                    canClimb = false;
                    foreach (var neighbour in neighbours)
                    {
                        var functionValue = TestFunction(neighbour);
                        if (functionValue > bestScore)
                        {
                            bestScore = functionValue;
                            bestIndex = (int)neighbour.ConvertToDouble();
                            canClimb = true;
                            currentNumber = neighbour;
                            break;
                        }
                    }
                }
                //mark all in the same pool
                foreach (var index in samePool)
                {
                    if (firstImprovement[index] <= bestScore)
                    {
                        firstImprovement[index] = bestScore;
                    }
                }
                if (!firstImprovementDictionary.ContainsKey(bestScore))
                {
                    firstImprovementDictionary.Add(bestScore, samePool);
                }
                else
                {
                    firstImprovementDictionary[bestScore].AddRange(samePool);
                }
                bestImprovementDictionary[bestScore].Insert(0, samePool.Last());

            }
            Print(false);
        }

    }

    public class BinaryRepresentation
    {
        private BitArray _numberBitArray;
        private const int NUMBER_OF_BITS = 5;

        public BinaryRepresentation(BitArray source)
        {
            _numberBitArray = new BitArray(NUMBER_OF_BITS);
            for (int i = 0; i < NUMBER_OF_BITS; i++)
            {
                _numberBitArray[i] = source[i];
            }
        }

        public BinaryRepresentation GetNewByFlipping(int index)
        {
            if (index < 0 || index > 4)
            {
                throw new IndexOutOfRangeException();
            }
            var newVal = new BinaryRepresentation(_numberBitArray);
            newVal.FipBit(index);
            return newVal;
        }

        public void FipBit(int index)
        {
            if (index < 0 || index > 4)
            {
                throw new IndexOutOfRangeException();
            }
            _numberBitArray[index] = !_numberBitArray[index];
        }

        public double ConvertToDouble()
        {
            int sum = 0;
            for (int convertPos = 0; convertPos < NUMBER_OF_BITS; convertPos++)
            {
                if (_numberBitArray[convertPos])
                {
                    sum += (int)Math.Pow(2, convertPos);
                }
            }
            return sum;
        }

    }
}
