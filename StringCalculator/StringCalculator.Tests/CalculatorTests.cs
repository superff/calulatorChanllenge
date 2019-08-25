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
        public void Test3Number_invalid()
        {
            string testCase = "5,tytyt5,5";

            int result = _calculator.Calculate(testCase);

            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void Test3Number_invalid_2()
        {
            string testCase = "5,5,ty5";

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
        public void Test10Number_n_3()
        {
            string testCase1 = "5,tytyt\n123,1,01,123,1,7,9,3c";

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

        [TestMethod]
        public void Test3Number_n_TopLimit_1001_end()
        {
            string testCase1 = "2,6,1001";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void Test2Number_CustomDelimiter()
        {
            string testCase1 = "//;\n2;5";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void Test2Number_CustomDelimiter_2()
        {
            string testCase1 = "//;\n2;5;";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void Test2Number_CustomDelimiter_3()
        {
            string testCase1 = "//;\n2;5;10;3";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(20, result);
        }

        [TestMethod]
        public void Test2Number_CustomDelimiter_4()
        {
            string testCase1 = "//?\n2;5;";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Test2Number_CustomDelimiter_5()
        {
            string testCase1 = "//?\n2?5;?35?20";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(57, result);
        }


        [TestMethod]
        public void Test2Number_CustomDelimiter_6()
        {
            string testCase1 = "//\t\n2\t5\t5";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(12, result);
        }


        [TestMethod]
        public void Test2Number_CustomDelimiter_None()
        {
            string testCase1 = "//\n2,5,5";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(12, result);
        }

        [TestMethod]
        public void Test3Number_CustomDelimiters_Any()
        {
            string testCase1 = "//[***]\n11***22***33";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(66, result);
        }

        [TestMethod]
        public void Test3Number_CustomDelimiters_Any_Speacial()
        {
            string testCase1 = "//[***]\n11t***22***33";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(55, result);
        }

        [TestMethod]
        public void Test3Number_CustomDelimiters_Any_Speacial_2()
        {
            string testCase1 = "//[***]\n11t***22***3t3";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(22, result);
        }

        [TestMethod]
        public void Test3Number_CustomDelimiters_Any_Speacial_3()
        {
            string testCase1 = "//[***]\n11t***22***33t";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(22, result);
        }

        [TestMethod]
        public void Test3Number_CustomDelimiters_2_Any()
        {
            string testCase1 = "//[!!]\n11t!!22!!!33t";

            int result = _calculator.Calculate(testCase1);

            Assert.AreEqual(22, result);
        }

        [TestMethod]
        public void Test3Number_CustomDelimiters_3_Any()
        {
            string testCase1 = "//[!!]\n11!!22!!33!2";

            int result = _calculator.Calculate(testCase1);
            Assert.AreEqual(33, result);
        }

        [TestMethod]
        public void Test3Number_CustomDelimiters_4_Any()
        {
            string testCase1 = "//[abc]\n11abc22abc33,33\n1abc1efg";

            int result = _calculator.Calculate(testCase1);
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void TestMultipleDelimiters()
        {
            string testCase1 = "//[*][!!][rrr]\n11rrr22*33!!44";

            int result = _calculator.Calculate(testCase1);
            Assert.AreEqual(110, result);
        }

        [TestMethod]
        public void TestMultipleDelimiters_2()
        {
            string testCase1 = "//[*][!!][rrr][]\n11rrr22*33!!44";

            int result = _calculator.Calculate(testCase1);
            Assert.AreEqual(110, result);
        }

        [TestMethod]
        public void TestMultipleDelimiters_3()
        {
            string testCase1 = "//[*][!!][rrr][]\n";

            int result = _calculator.Calculate(testCase1);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestMultipleDelimiters_4()
        {
            string testCase1 = "//[*][!!][rrr][]\n456";

            int result = _calculator.Calculate(testCase1);
            Assert.AreEqual(456, result);
        }

        [TestMethod]
        public void TestMultipleDelimiters_5()
        {
            string testCase1 = "//[*][!!][rrr][ ]\n45,45\n4*6!!50rrr50 100";

            int result = _calculator.Calculate(testCase1);
            Assert.AreEqual(300, result);
        }

        [TestMethod]
        public void TestMultipleDelimiters_Exception()
        {
            try
            {
                string testCase1 = "//[*][!!][rrr][\n11rrr22*33!!44";

                int result = _calculator.Calculate(testCase1);
            }
            catch(Exception ex)
            {
                Assert.AreEqual("no closing ]", ex.Message);
            }
        }

        [TestMethod]
        public void TestMultipleDelimiters_Exception2()
        {
            try
            {
                string testCase1 = "//[*][!!][rrr]";
                int result = _calculator.Calculate(testCase1);
                Assert.AreEqual(0, result);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("\\n is missing", ex.Message);
            }
        }

        [TestMethod]
        public void TestMultipleDelimiters_Exception3()
        {
            try
            {
                string testCase1 = "//[*][!!][rrr]234*134";
                int result = _calculator.Calculate(testCase1);
                Assert.AreEqual(0, result);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("\\n is missing", ex.Message);
            }
        }
    }
}
