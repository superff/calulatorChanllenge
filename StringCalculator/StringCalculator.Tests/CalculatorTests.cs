using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringCalculator.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        private ICalculator _calculator;

        public CalculatorTests()
        {
            _calculator = new StringCalculator();
        }

        [TestMethod]
        public void Test2Number_1()
        {
            string testCase = "1,20";

            int result = _calculator.Calculate(testCase);

            Assert.AreEqual(21, result);
        }

        [TestMethod]
        public void Test2Number_2()
        {
            string testCase = "5000";

            int result = _calculator.Calculate(testCase);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Test2Number_3()
        {
            string testCase = "\"\"";

            int result = _calculator.Calculate(testCase);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Test2Number_4()
        {
            string testCase = "5,tytyt";

            int result = _calculator.Calculate(testCase);

            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Test3Number_1()
        {
            string testCase = "5,tytyt,5";

            int result = _calculator.Calculate(testCase);

            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void Test3Number_2()
        {
            string testCase1 = "5,tytyt,123";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(128, result);
        }

        [TestMethod]
        public void Test10Number_1()
        {
            string testCase1 = "5,tytyt,123,1,01,123,1,7,9,fredadc";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(270, result);
        }

        [TestMethod]
        public void Test10Number_1DoubleDelimiter()
        {
            string testCase1 = "5,tytyt,123,1,01,123,1,7,9,,,fredadc";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(270, result);
        }

        [TestMethod]
        public void Test3Number_n_1()
        {
            string testCase1 = "1\n2,3";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Test3Number_n_2()
        {
            string testCase1 = "1,2\n3";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Test10Number_n_2()
        {
            string testCase1 = "5,tytyt\n123,1,01,123,1,7,9,fredadc";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(270, result);
        }

        [TestMethod]
        public void Test10Number_n_2DoubleDelimiter()
        {
            string testCase1 = "5,tytyt\n\n123,1,01,123,1,7,9,fredadc";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(270, result);
        }

        [TestMethod]
        public void Test10Number_n_3DoubleDelimiter()
        {
            string testCase1 = "5,tytyt\n\n123,1,01,123,123e,1,7,9,fredadc";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(270, result);
        }

        [TestMethod]
        public void Test10Number_4DNegative()
        {
            try
            {
                string testCase1 = "5,tytyt\n\n123,1,01,-123,123e,1,-7,9,-fredadc,-34";

                int result = _calculator.Calculate(testCase1);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("-123,-7,-34", ex.Message);
            }
        }

        [TestMethod]
        public void Test10Number_4DNegative2()
        {
            try
            {
                string testCase1 = "5,tytyt\n\n123,1,01,-123,123e,-11,-7,9,-fredadc";

                int result = _calculator.Calculate(testCase1);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("-123,-11,-7", ex.Message);
            }
        }


        [TestMethod]
        public void Test10Number_n_5TopLimit()
        {
            string testCase1 = "5,tytyt\n\n123,12567890,1,01,123,123e,1,7,9,fredadc,1234";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(270, result);
        }

        [TestMethod]
        public void Test10Number_n_5TopLimit_1000()
        {
            string testCase1 = "5,tytyt\n\n123,1000,12567890,1,01,123,123e,1,7,9,fredadc,1234";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(1270, result);
        }

        [TestMethod]
        public void Test3Number_n_TopLimit_1001()
        {
            string testCase1 = "2,1001,6";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(8, result);
        }

    }
}
