using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestApp.NUnit
{
    public abstract class TestAppBase
    {
        /// <summary>
        /// Test samples data loaded from json file
        /// </summary>
        protected dynamic TestData { get; private set; }

        /// <summary>
        /// Get the tested array by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected int[] GetTestArray(int index) => TestData.TestArray[index];

        /// <summary>
        /// Seracher instance
        /// </summary>
        protected Searcher Searcher { get; } = new Searcher();

        /// <summary>
        /// Test File Path
        /// </summary>
        public string TestDataFile { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestArrays.json");

        /// <summary>
        /// Setup test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            TestData = JsonConvert.DeserializeAnonymousType(File.ReadAllText(TestDataFile), new
            {
                TestArray = new List<int[]> { new int[0] }

            });
            OnSetup();
        }

        /// <summary>
        /// Tear down test
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            OnTearDown();
        }

        /// <summary>
        /// Do its own setup for derived class
        /// </summary>

        protected virtual void OnSetup() { }

        /// <summary>
        /// Do its own tear down for derived class
        /// </summary>
        protected virtual void OnTearDown() { }

    }
}
