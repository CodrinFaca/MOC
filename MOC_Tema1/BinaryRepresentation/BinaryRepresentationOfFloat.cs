using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryRepresentation
{
    public class BinaryRepresentationOfFloat
    {
        public BitArray BinaryRepresentation { get; set; }

        private double _upperLimit;
        private double _lowerLimit;
        private int _bitSize;
        private double _maxValue;

        public static void CrossOver(int position, ref BitArray first, ref BitArray second)
        {
            var initialFirst = new BitArray(first);
            var initialSecond = new BitArray(second);

            for (int i = 0; i <= position; i++)
            {
                initialFirst[i] = second[i];
                initialSecond[i] = first[i];
            }

            first = initialFirst;
            second = initialSecond;
        }

        public BinaryRepresentationOfFloat(int bitSize, double lowerLimit, double upperLimit)
        {
            _upperLimit = upperLimit;
            _lowerLimit = lowerLimit;
            _bitSize = bitSize;

            //if i dont initialize it like this, it breaks
            BitArray arr = new BitArray(bitSize);
            for (int i = 0; i < bitSize; i++)
            {
                arr.Set(i, true);
            }
            //byte[] data = new byte[1];
            //arr.CopyTo(data, 0);
            _maxValue = ToInt(arr, bitSize);
            //_maxValue = ConvertLittleEndian(data);

            //initialize by random
            BinaryRepresentation = new BitArray(bitSize);
            Random rnd = new Random();
            for (int i = 0; i < bitSize; i++)
            {
                BinaryRepresentation.Set(i, rnd.NextDouble() > 0.5);
            }

        }

        public BinaryRepresentationOfFloat Clone()
        {
            var result = new BinaryRepresentationOfFloat(_bitSize, _lowerLimit, _upperLimit);
            result.BinaryRepresentation = (BitArray)BinaryRepresentation.Clone();

            return result;
        }
        public void FlipBit(int position)
        {
            BinaryRepresentation.Set(position, !BinaryRepresentation[position]);

        }
        public BinaryRepresentationOfFloat GetNewValueByFlippingBit(int position)
        {

            var newVal = this.Clone();
            newVal.FlipBit(position);
            return newVal;
        }
        public double ConvertToDouble(int offset = 0)
        {
            int sum = 0;
            for(int convertPos = 0;convertPos < _bitSize ; convertPos ++)
            {
                if (BinaryRepresentation[convertPos])
                {
                    sum += (int)Math.Pow(2, convertPos);
                }
            }
            
            int segment = sum;
            double rate = (double)segment / _maxValue;
            var resultDouble = _lowerLimit + (rate * (_upperLimit - _lowerLimit));

            // get next / prev number by offset
            if ((resultDouble != _upperLimit && offset < 0) ||
              (resultDouble != _lowerLimit && offset > 0))
            {
                resultDouble += (rate * offset);
            }
            return resultDouble;

        }

        private int ConvertLittleEndian(byte[] array)
        {
            int pos = 0;
            int result = 0;
            foreach (byte by in array)
            {
                result |= ((int)by) << pos;
                pos += 8;
            }
            return result;
        }


        public List<BinaryRepresentationOfFloat> GetNeighbourhood()
        {
            List<BinaryRepresentationOfFloat> result = new List<BinaryRepresentationOfFloat>();
            for (int i = 0; i < _bitSize; i++)
            {
                result.Add(GetNewValueByFlippingBit(i));
            }
            return result;
        }

        private int ToInt(BitArray ba, int size)
        {
            int sum = 0;
            for (int convertPos = 0; convertPos < size; convertPos++)
            {
                if (ba[convertPos])
                {
                    sum += (int)Math.Pow(2, convertPos);
                }
            }
            return sum;
        }
    }
}
