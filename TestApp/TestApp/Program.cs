﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp
{
    class Program
    {

        /// <summary>
        /// Main entry
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // generate a random array length 100, all elements value are 0 to 100
            var array = GenerateAndPrintRandomArray(new Random(), 50);

            // assume given number is 100
            var number = 100;

            var (first, second, third) = ThreeSearch(array, number);

            if (first != -1 && second != -1 && third != -1)
            {
                Console.WriteLine($"\n\nFound \narray[{first}]={array[first]}\narray[{second}]={array[second]}\narray[{third}]={array[third]}");
                Console.WriteLine($"meets array[{first}] + array[{second}] + array[{third}] = {number}");
            }
            else
            {
                Console.WriteLine("\n\nFound no suitable elements");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Search the suitable three indexes that meet array[i] + array[j] + array[k] = number.
        /// If found no result, return (-1, -1, -1)
        /// </summary>
        /// <param name="array">Source array</param>
        /// <param name="number">The given number</param>
        /// <returns></returns>
        static (int, int, int) ThreeSearch(int[] array, int number)
        {
            var (first, second, third) = (-1, -1, -1);

            // require more than 3 elements
            if (array.Length < 3)
            {
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
                    first = i;
                    second = subFirst;
                    third = subSecond;
                    break;
                }
            }
            return (first, second, third);
        }

        /// <summary>
        /// Search the suitable two indexes that meet array[i] + array[j] = number and any of them not be duplicated with the give index.
        /// If found no result, return (-1, -1)
        /// </summary>
        /// <param name="array">Source array</param>
        /// <param name="number">the given number</param>
        /// <param name="index">the give index</param>
        /// <returns></returns>
        static (int, int) TwoSearch(int[] array, int number, int index)
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

        /// <summary>
        /// Generate a random array and print its elements
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length">array length</param>
        /// <returns></returns>
        static int[] GenerateAndPrintRandomArray(Random random, int length)
            => Enumerable.Range(0, length).Select(i =>
            {
                var element = random.Next(0, 100);
                // print element
                Console.WriteLine($"array[{i}]={element}");
                return element;
            }).ToArray();
    }
}
