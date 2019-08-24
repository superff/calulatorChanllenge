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
        : ICalculator
    {
        private const int topLimit = 1000;
        private HashSet<char> delimiters;

        public StringCalculator()
        {
            delimiters = new HashSet<char>();
            delimiters.Add(',');
            delimiters.Add('\n');
        }

        /// <summary>
        /// Allow negative number or not
        /// </summary>
        public bool AllowNegative { get; set; } = false;

        /// <summary>
        /// Calculate the sum of the input string
        /// </summary>
        /// <param name="stringInput">the input string</param>
        /// <returns></returns>
        public int Calculate(string stringInput)
        {
            if (string.IsNullOrEmpty(stringInput))
            {
                return 0;
            }

            int result = 0;
            int number = 0;
            bool isNegative = false;
            bool isGreaterThanTop = false;
            var negativeNumbers = new List<int>();
            var sb = new StringBuilder();

            // get the customs delimiter
            int startIndex = 0;
            if (containsCustomDelimiters(stringInput))
            {
                delimiters.Add(stringInput[2]);
                startIndex = 4;
            }

            for (int i = startIndex; i < stringInput.Length; i++)
            {
                char letter = stringInput[i];

                // check delimiter first
                if (IsDelimiter(letter))
                {
                    result += number;

                    if (number < 0)
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
                else if (letter == '-')
                {
                    isNegative = true;
                }
                else if (char.IsDigit(letter))
                {
                    if (isGreaterThanTop)
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
                    if (isNegative && number != 0)
                    {
                        number = 0 - number;
                        isNegative = false;
                    }

                    // check number is greater than top
                    if (number > topLimit)
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
            if (number < 0)
            {
                negativeNumbers.Add(number);
            }

            if (negativeNumbers.Count != 0 && !AllowNegative)
            {
                throw new ArgumentException(string.Join(",", negativeNumbers));
            }

            var finalResult = result + number;
            sb.Append($"{number}={finalResult}");

            Console.WriteLine(sb);
            return finalResult;
        }

        public bool IsDelimiter(char letter)
        {
            return delimiters.Contains(letter);
        }

        public bool containsCustomDelimiters(string str)
        {
            return str.Length > 4
                && str.Substring(0, 2) == "//"
                && str[3] == '\n';
        }
    }
}
