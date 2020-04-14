// <copyright file="PositiveBitCounter.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Algorithms.CountingBits
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PositiveBitCounter
    {
        /// <summary>
        /// Function to determine the 1-bit indexes and their quantity in an integer
        /// </summary>
        /// <param name="input">Integer on which to calculate 1-bit indexes and quantity</param>
        /// <returns>IEnumerable with quantity and index of 1-bit</returns>
        public IEnumerable<int> Count(int input)
        {
            if (input < 0)
                throw new ArgumentException("The value cannot be negative");
            
            var result = new List<int>();
            var indexes = GetPositiveIndexes(input);

            result.Add(indexes.Count);
            result.AddRange(indexes);

            return result;
        }

        /// <summary>
        /// Function to determine the 1-bit indexes from an integer
        /// </summary>
        /// <param name="value">Integer on which to calculate 1-bit indexes</param>
        /// <returns>List of 1-bit indexes</returns>
        private List<int> GetPositiveIndexes(int value)
        {
            int bitsLength = GetBitsLength(value);
            var idIndexes = new List<int>();
            for (var j = 0; j < bitsLength; j++)
            {
                if ((value & (1 << j)) != 0)
                    idIndexes.Add(j);
            }
            return idIndexes;
        }

        /// <summary>
        /// Function to calculate the bits length of an integer to decrease the cost of the algorithm
        /// </summary>
        /// <param name="value">Integer on which to calculate the bits length</param>
        /// <returns>Bits Length</returns>
        private int GetBitsLength(int value)
        {
            if (value < 256)
                return 8; //1 Byte
            else if (value < 65536)
                return 16; //2 Bytes
            else if (value < 16777216)
                return 24; //3 Bytes
            else
                return 32; //4 Bytes -> Int32.MaxValue
        }
    }
}
