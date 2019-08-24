using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StringCalculator
{
    /// <summary>
    /// String Calculator
    /// </summary>
    public class StringCalculator
        :ICalculator
    {
        private const int topLimit = 1000;

        public StringCalculator()
        {
        }

        /// <summary>
        /// Calculate the sum of the input string
        /// </summary>
        /// <param name="stringInput">the input string</param>
        /// <returns></returns>
        public int Calculate(string stringInput)
        {
            int result = 0;
            int number = 0;
            bool isNegative = false;
            bool isGreaterThanTop = false;
            List<int> negativeNumbers = new List<int>();
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < stringInput.Length; i++)
            {
                char letter = stringInput[i];

                // check delimiter first
                if(IsDelimiter(letter))
                {
                    result += number;

                    if(number < 0)
                    {
                        negativeNumbers.Add(number);
                    }

                    sb.Append($"{number}+");

                    // reset
                    isNegative = false; 
                    number = 0;
                    isGreaterThanTop = false;
                }
                //negative number
                else if(letter == '-')
                {
                    isNegative = true;
                }
                else if (char.IsDigit(letter))
                {
                    if(isGreaterThanTop)
                    {
                        continue;
                    }

                    if (number >= 0)
                    {
                        number = number * 10 + (letter - '0');
                    }
                    else
                    {
                        number = number * 10 - (letter - '0');
                    }


                    // first negative numbers
                    if(isNegative && number != 0) 
                    {
                        number = 0 - number;
                        isNegative = false;
                    }

                    // check number is greater than top
                    if(number > topLimit)
                    {
                        isGreaterThanTop = true;
                        number = 0;
                    }
                }
                // for special chars
                else
                {
                    number = 0;
                }
            }

            // the last number
            if(number <0)
            {
                negativeNumbers.Add(number);
            }

            if(negativeNumbers.Count != 0)
            {
                throw new ArgumentException(string.Join(",",negativeNumbers));
            }

            var finalResult = result + number;
            sb.Append($"{number}={finalResult}");

            Console.WriteLine(sb);
            return finalResult;
        }

        public bool IsDelimiter(char letter)
        {
            return letter == ',' || letter =='\n';
        }
    }
}
