using System;
using System.Collections.Generic;
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
        private bool isNegative;
        private bool hasSpecialChar;
        private bool isGreaterThanTop;
        private List<int> negativeNumbers;
        private HashSet<char> delimiters;
        private StringBuilder sb;

        public StringCalculator()
        {
            delimiters = new HashSet<char>();
            delimiters.Add(',');
            delimiters.Add('\n');
            negativeNumbers = new List<int>();
            sb = new StringBuilder();
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

            // sum
            int result = 0;
            int number = 0;

            int startIndex = GetNewIndexAndFetchDelimiter(stringInput);

            for (int i = startIndex; i < stringInput.Length; i++)
            {
                char letter = stringInput[i];

                // check delimiter first
                if (IsDelimiter(letter))
                {
                    CalculateResult(ref result, ref number);
                }
                //negative number
                else if (letter == '-')
                {
                    isNegative = true;
                }
                else if (char.IsDigit(letter))
                {
                    GetCurrentNumber(ref number, letter);
                }
                // for special chars
                else
                {
                    hasSpecialChar = true;
                    number = 0;
                }
            }

            return CalculateFinalResult(ref result, number);
        }

        /// <summary>
        /// Fetch the customs delimiter
        /// </summary>
        /// <param name="stringInput"></param>
        /// <returns>the start index of string to calculate</returns>
        private int GetNewIndexAndFetchDelimiter(string stringInput)
        {
            // get the customs delimiter
            int startIndex = 0;
            if (ContainsCustomDelimiters(stringInput))
            {
                delimiters.Add(stringInput[2]);
                startIndex = 4;
            }

            return startIndex;
        }

        public bool IsDelimiter(char letter)
        {
            return delimiters.Contains(letter);
        }

        public bool ContainsCustomDelimiters(string str)
        {
            return str.Length > 4
                && str.Substring(0, 2) == "//"
                && str[3] == '\n';
        }

        private void CalculateResult(ref int result, ref int number)
        {
            if (hasSpecialChar)
            {
                number = 0;
            }

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
            hasSpecialChar = false;
        }

        private void GetCurrentNumber(ref int number, char letter)
        {
            if (isGreaterThanTop || hasSpecialChar)
            {
                return;
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

        private int CalculateFinalResult(ref int result, int number)
        {
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
    }
}
