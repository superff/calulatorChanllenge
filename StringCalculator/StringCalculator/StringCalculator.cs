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
        private HashSet<string> delimiters;
        private StringBuilder outputStringBuilder;
        private StringBuilder unknownChars;

        public StringCalculator()
        {
            delimiters = new HashSet<string>
            {
                ",",
                "\n"
            };

            negativeNumbers = new List<int>();
            outputStringBuilder = new StringBuilder();
            unknownChars = new StringBuilder();
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

            int startIndex = GetStartIndexAndFetchDelimiter(stringInput);

            for (int i = startIndex; i < stringInput.Length; i++)
            {
              
                char letter = stringInput[i];
                unknownChars.Append(letter);

                // check delimiter first
                if (IsDelimiter(unknownChars.ToString(),ref number))
                {
                    CalculateResult(ref result, ref number);
                    unknownChars.Clear();
                }
                //negative number
                else if (letter == '-')
                {
                    isNegative = true;
                    unknownChars.Clear();
                }
                else if (char.IsDigit(letter))
                {
                    // check if special char is in the delimter
                    if(unknownChars.Length > 1)
                    {
                        hasSpecialChar = true;
                    }

                    GetCurrentNumber(ref number, letter);
                    unknownChars.Clear();
                }
            }

            if(unknownChars.Length != 0)
            {
                hasSpecialChar = true;
                number = 0;
            }

            return CalculateFinalResult(ref result, number);
        }

        /// <summary>
        /// Fetch the customs delimiter
        /// </summary>
        /// <param name="stringInput"></param>
        /// <returns>the start index of string to calculate</returns>
        private int GetStartIndexAndFetchDelimiter(string stringInput)
        {
            // get the customs delimiter
            int startIndex = 0;

            if(stringInput.Length >= 3
                && stringInput.Substring(0, 2) == "//")
            {
                if(stringInput[2] == '[')
                {
                    int delimiterEndIndex = 3, delimiterStartIndex= 3;

                    while (delimiterEndIndex < stringInput.Length)
                    {
                        if(stringInput[delimiterEndIndex] != ']')
                        {
                            delimiterEndIndex++;
                        }
                        else
                        {
                            // no closing ] and \n found
                            if(delimiterEndIndex + 1 == stringInput.Length || stringInput[delimiterEndIndex + 1] != '\n')
                            {
                                return startIndex;
                            }

                            if(delimiterStartIndex < delimiterEndIndex)
                            {
                                delimiters.Add(
                                    stringInput.Substring(
                                        delimiterStartIndex, delimiterEndIndex - delimiterStartIndex));

                                // because "]\n" will be the next two chars 
                                return delimiterEndIndex + 2;
                            }
                        }
                    }
                }
                else
                {
                    if (stringInput[2] == '\n')
                    {
                        startIndex = 3;
                        return startIndex;
                    }

                    if (stringInput[3] == '\n')
                    {
                        delimiters.Add(stringInput[2].ToString());
                        startIndex = 4;
                        return startIndex;
                    }
                }
            }

            return startIndex;
        }

        public bool IsDelimiter(string str,ref int number)
        {
            if(delimiters.Contains(str))
            {
                return true;
            }

            // need to check substring , for example "t***"
            for(int i = 1; i < str.Length; i++)
            {
                if (delimiters.Contains(str.Substring(i)))
                {
                    number = 0;
                    return true;
                }
            }

            return false;
        }

        private void CalculateResult(ref int result, ref int number)
        {
            result += number;

            if (number < 0)
            {
                negativeNumbers.Add(number);
            }

            outputStringBuilder.Append($"{number}+");

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
                number = 0;
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
            outputStringBuilder.Append($"{number}={finalResult}");
            Console.WriteLine(outputStringBuilder);
            return finalResult;
        }
    }
}
