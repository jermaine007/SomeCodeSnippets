using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestApp
{
    public class Searcher
    {
        public Random Random { get; private set; }

        public Searcher() => this.Random = new Random();

        /// <summary>
        /// Search the suitable three indexes that meet array[i] + array[j] + array[k] = number.
        /// If found no result, return (-1, -1, -1)
        /// </summary>
        /// <param name="array">Source array</param>
        /// <param name="number">The given number</param>
        /// <returns></returns>
        public (int, int, int) ThreeSearch(int[] array, int number, Action<string> output = null)
        {
            var (first, second, third) = (-1, -1, -1);

            // require more than 3 elements
            if (array.Length < 3)
            {
                return (first, second, third);
            }

            // check the if given number is out of bounds
            if (!CheckGivenNumberBounds(array, number))
            {
                output?.Invoke($"The given number {number} is out of bounds.");
                return (first, second, third);
            }

            for (int i = 0; i < array.Length; i++)
            {
                // divide number into two parts: current element and the remain part would be the sum of the left two elements.
                var sub = number - array[i];
                // search the left two elements to check if it meet the condition. 
                var (subFirst, subSecond) = TwoSearch(array, sub, i);

                if (subFirst != -1 && subSecond != -1)
                {
                    // reorder indexes
                    (first, second, third) = Reorder((subFirst, subSecond, i));
                    output?.Invoke($"Found {(first, second, third) }");
                    break;
                }
            }
            return (first, second, third);
        }

        /// <summary>
        /// Another search method
        /// </summary>
        /// <param name="array"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public (int, int, int) AnotherSearchThree(int[] array, int number, Action<string> output = null)
        {
            var (first, second, third) = (-1, -1, -1);

            // require more than 3 elements
            if (array.Length < 3)
            {
                return (first, second, third);
            }

            // check the if given number is out of bounds
            if (!CheckGivenNumberBounds(array, number))
            {
                output?.Invoke($"The given number {number} is out of bounds.");
                return (first, second, third);
            }

            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    var remain = number - (array[i] + array[j]);
                    var index = FindIndex(array, i, j, remain);
                    if (index != -1)
                    {
                        (first, second, third) = Reorder((i, j, index));
                        output?.Invoke($"Found {(first, second, third) }");
                        return (first, second, third);
                    }
                }

            }
            return (first, second, third);
        }

        private int FindIndex(int[] array, int i, int j, int remain)
        {
            int index = -1;
            index = Array.IndexOf(array, remain);
            if (index == -1)
            {
                return index;
            }
            if (index == i && i + 1 < array.Length)
            {
                index = Array.IndexOf(array, remain, i + 1);
            }
            if (index == -1)
            {
                return index;
            }
            if (index == j && j + 1 < array.Length)
            {
                index = Array.IndexOf(array, remain, j + 1);
            }
            return index;
        }



        /// <summary>
        /// Generate a random array and print its elements
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length">array length</param>
        /// <returns></returns>
        public int[] GenerateRandomArray(int length, int minValue, int maxValue)
            => Enumerable.Range(0, length).Select(i => Random.Next(minValue, maxValue)).ToArray();


        /// <summary>
        /// Reorder the output indexes
        /// </summary>
        /// <param name="indexes">The indexes</param>
        /// <returns></returns>
        private (int, int, int) Reorder((int, int, int) indexes)
        {
            var ordered = new List<int> { indexes.Item1, indexes.Item2, indexes.Item3 }.OrderBy(i => i).ToArray();
            return (ordered[0], ordered[1], ordered[2]);
        }

        /// <summary>
        /// Check if the given number out of bounds
        /// The given number must be greater than the sum of min three numbers and less than sum of max three numbers
        /// </summary>
        /// <param name="array">The input array</param>
        /// <param name="number">The given number</param>
        /// <returns>Return true if validated, otherwise false</returns>
        private bool CheckGivenNumberBounds(int[] array, int number)
        {
            var ordered = array.OrderBy(e => e);
            return ordered.Take(3).Sum() <= number && ordered.TakeLast(3).Sum() >= number;
        }

        /// <summary>
        /// Search the suitable two indexes that meet array[i] + array[j] = number and any of them not be duplicated with the give index.
        /// If found no result, return (-1, -1)
        /// </summary>
        /// <param name="array">Source array</param>
        /// <param name="number">the given number</param>
        /// <param name="index">the give index</param>
        /// <returns></returns>
        private (int, int) TwoSearch(int[] array, int number, int index)
        {
            var (first, second) = (-1, -1);
            // map to store the value and index.
            // If given number - current element does not exist, store it and its index.
            // Otherwise, to check if the founded element's index is same with the given index.
            var map = new Dictionary<int, int>();

            for (int i = 0; i < array.Length; i++)
            {
                if (index == i)
                {
                    continue;
                }

                var sub = number - array[i];
                if (!map.ContainsKey(sub))
                {
                    // due to duplicated elements and only required to return one index, do not add or update the stored index.
                    if (!map.ContainsKey(array[i]))
                    {
                        map.Add(array[i], i);
                    }
                }
                else
                {
                    var foundedIndex = map[sub];

                    // same with the given index, continue to search next one
                    if (i == index || foundedIndex == index)
                    {
                        continue;
                    }

                    first = foundedIndex;
                    second = i;
                    break;
                }
            }
            return (first, second);
        }
    }
}
