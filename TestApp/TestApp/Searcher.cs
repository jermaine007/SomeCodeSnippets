using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestApp
{
    public class Searcher
    {
        public Searcher() { }

        /// <summary>
        /// Search the suitable three indexes that meet array[i] + array[j] + array[k] = number.
        /// If found no result, return (-1, -1, -1)
        /// </summary>
        /// <param name="array">Source array</param>
        /// <param name="number">The given number</param>
        /// <returns></returns>
        public (int, int, int) ThreeSearch(int[] array, int number)
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
                Console.WriteLine($"The given number {number} is out of bounds.");
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
                    break;
                }
            }
            return (first, second, third);
        }

        /// <summary>
        /// Reorder the output indexes
        /// </summary>
        /// <param name="indexes">The indexes</param>
        /// <returns></returns>
        private (int, int, int) Reorder((int, int, int) indexes)
        {
            // subFirst always < subSecond
            // given index < subFirst -> given index, subFirst, subSecond
            // given index > subSecond -> subFirst, subSecond, given index
            // Otherwise subFirst -> given index -> subSecond
            // Or use linq -> new List<int> { indexes.Item1, indexes.Item2, indexes.Item3 }.OrderBy(i => i).ToArray();
            if (indexes.Item3 < indexes.Item1)
            {
                return (indexes.Item3, indexes.Item1, indexes.Item2);
            }
            else if (indexes.Item3 < indexes.Item2)
            {
                return (indexes.Item1, indexes.Item2, indexes.Item3);
            }
            else
            {
                return (indexes.Item1, indexes.Item3, indexes.Item2);
            }
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
