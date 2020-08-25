using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.NUnit
{
    [TestFixture(Category = nameof(TestAppBase))]
    public class TestAppNunitTests : TestAppBase
    {
        /// <summary>
        /// array = [ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 ]
        /// number = 20
        /// Expected array[3] + array[8] + array[9] = 20
        /// Expected output is (3, 8, 9)
        /// </summary>
        [Test]
        public void TestFoundSuccess()
        {
            var testArray = this.GetTestArray(0);
            var number = 20;
            var result = Searcher.ThreeSearch(testArray, number);
            Assert.AreEqual((3, 8, 9), result);
        }

        /// <summary>
        /// array = [ 10, 11, 17, 22, 25, 46, 17, 18, 19, 10, 11, 1, 24, 31, 41, 56, 16, 17, 18, 19 ]
        /// number = 20
        /// Expected array[5] + array[7] + array[15] = 120
        /// Expected output is (5, 7, 15)
        /// </summary>
        [Test]
        public void TestFoundSuccess1()
        {
            var testArray = this.GetTestArray(1);
            var number = 120;
            var result = Searcher.ThreeSearch(testArray, number);
            Assert.AreEqual((5, 7, 15), result);
        }


        /// <summary>
        /// array = [ 22, 14, 17, 11, 1, -2, -1, -8, -4, -3, 11, 12, 81, 0, 2, 4, 1, -8, -10, 10, 6, 7, 10, 12, 19, 57, 12, 18, 3, 0 ]
        /// number = 20
        /// Expected array[0] + array[4] + array[9] = 20
        /// Expected output is (0, 4, 9)
        /// </summary>
        [Test]
        public void TestFoundSuccess2()
        {
            var testArray = this.GetTestArray(2);
            var number = 20;
            var result = Searcher.ThreeSearch(testArray, number);
            Assert.AreEqual((0, 4, 9), result);
        }

        /// <summary>
        /// array = [ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 ]
        /// number = 23
        /// Expected array[6] + array[8] + array[9] = 23
        /// Expected output is (6, 8, 9)
        /// </summary>
        [Test]
        public void TestFoundSuccess3()
        {
            var testArray = this.GetTestArray(0);
            var number = 23;
            var result = Searcher.ThreeSearch(testArray, number);
            Assert.AreEqual((6, 8, 9), result);
        }

        /// <summary>
        /// array = [ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 ]
        /// number = 12
        /// Expected array[0] + array[5] + array[7] = 12
        /// Expected output is (0, 5, 7)
        /// </summary>
        [Test]
        public void TestFoundSuccess4()
        {
            var testArray = this.GetTestArray(0);
            var number = 12;
            var result = Searcher.ThreeSearch(testArray, number);
            Assert.AreEqual((0, 5, 7), result);
        }


        /// <summary>
        /// array = [1, 2]
        /// Expected not found as elements are less than 3
        /// Expected output (-1, -1, -1)
        /// </summary>
        [Test]
        public void TestNotFound()
        {
            var testArray = this.GetTestArray(3);
            var number = 2;
            var result = Searcher.ThreeSearch(testArray, number);
            Assert.AreEqual((-1, -1, -1), result);
        }

        /// <summary>
        /// array = [ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 ]
        /// Expected not found as give number is greater than sum of max 3
        /// Expected output (-1, -1, -1) 
        /// </summary>
        [Test]
        public void TestNotFound1()
        {
            var testArray = this.GetTestArray(0);
            var number = 25;
            var result = Searcher.ThreeSearch(testArray, number);
            Assert.AreEqual((-1, -1, -1), result);
        }


        /// <summary>
        /// array = [ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 ]
        /// Expected not found as give number is greater than sum of max 3
        /// Expected output (-1, -1, -1) 
        /// </summary>
        [Test]
        public void TestNotFound2()
        {
            var testArray = this.GetTestArray(0);
            var number = 1;
            var result = Searcher.ThreeSearch(testArray, number);
            Assert.AreEqual((-1, -1, -1), result);
        }

        /// <summary>
        /// array = [ 13, 34, 32, 16, 29, 26, 2, 25, 5, 18 ]
        /// Expect could not found the given number 100
        /// Excpected output is (-1, -1, -1)
        /// </summary>
        [Test]
        public void TestNotFound3()
        {
            var testArray = this.GetTestArray(4);
            var number = 100;
            var result = Searcher.ThreeSearch(testArray, number);
            Assert.AreEqual((-1, -1, -1), result);
        }

        /// <summary>
        /// array = [ 13, 34, 32, 16, 29, 26, 2, 25, 5, 18 ]
        /// Expect could not found the given number 100
        /// Excpected output is (-1, -1, -1)
        /// </summary>
        [Test]
        public void TestNotFound4()
        {
            var testArray = this.GetTestArray(4);
            var number = 97;
            var result = Searcher.ThreeSearch(testArray, number);
            Assert.AreEqual((-1, -1, -1), result);
        }
    }
}
