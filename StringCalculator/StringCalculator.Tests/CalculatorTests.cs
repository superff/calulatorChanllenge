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

            Assert.AreEqual(5000, result);
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
    }
}
