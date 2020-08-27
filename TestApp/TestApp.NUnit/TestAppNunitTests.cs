﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace TestApp.NUnit
{
    [TestFixture(Category = nameof(TestAppNunitTests))]
    public class TestAppNunitTests : TestAppBase
    {
        private static readonly (int, int, int) NO_RESULT = (-1, -1, -1);

        [Test]
        public void TestJsonFile()
        {
            foreach (var data in TestData.Data)
            {
                var array = (int[])data.Array;
                var number = (int)data.Number;
                var (expectedFirst, expectedSecond, expectedThird) = ((int, int, int))(data.Result[0], data.Result[1], data.Result[2]);
                var (actualFirst, actualSecond, actualThird) = Searcher.ThreeSearch(array, number, msg => TestContext.WriteLine(msg));
                Assert.IsTrue((expectedFirst, expectedSecond, expectedThird) == (actualFirst, actualSecond, actualThird));
                if ((actualFirst, actualSecond, actualThird) != NO_RESULT)
                {
                    Assert.AreEqual(number, array[actualFirst] + array[actualSecond] + array[actualThird]);
                }
            }
        }

        [Test]
        public void TestRandomTestCases()
        {
            int arrayNumber = Searcher.Random.Next(100000, 1000000);
            while (arrayNumber > 0)
            {
                DoRandomTest();
                arrayNumber--;
            }
        }

        private void DoRandomTest()
        {

            var length = Searcher.Random.Next(4, 10000);
            var number = Searcher.Random.Next();
            var array = Searcher.GenerateRandomArray(length, int.MinValue / 3, int.MaxValue / 3);
            var (actualFirst, actualSecond, actualThird) = Searcher.ThreeSearch(array, number, msg => TestContext.WriteLine(msg));
            TestContext.WriteLine($"Run test array length: {length}, number: {number}");

            if ((actualFirst, actualSecond, actualThird) != NO_RESULT)
            {
                Assert.AreEqual(number, array[actualFirst] + array[actualSecond] + array[actualThird]);
            }
            else
            {
                // search again
                (actualFirst, actualSecond, actualThird) = Searcher.AnotherSearchThree(array, number, msg => TestContext.WriteLine(msg));
                Assert.AreEqual(NO_RESULT, (actualFirst, actualSecond, actualThird));
            }

        }
    }
}
