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
        /// Seracher instance
        /// </summary>
        protected Searcher Searcher { get; } = new Searcher();

        /// <summary>
        /// Test File Path
        /// </summary>
        public string TestDataFile { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData.json");

        /// <summary>
        /// Setup test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            TestData = JsonConvert.DeserializeAnonymousType(File.ReadAllText(TestDataFile), new
            {
                Data = new[] {
                    new {
                     Array = new int[0],
                     Number = 0,
                     Result = new int[0]
                    }
                }

            }); ;
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
