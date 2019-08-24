using System;
using System.Collections.Generic;

namespace StringCalculator
{
    public class StringCalculator
        :ICalculator
    {
        public StringCalculator()
        {
        }

        public int Calculate(string stringInput)
        {
            int result = 0;
            int number = 0;
            char previousletter = '0';
            bool isNegative = false;
            List<int> negativeNumbers = new List<int>();

            for(int i = 0; i < stringInput.Length; i++)
            {
                char letter = stringInput[i];

                if(IsDelimiter($"{previousletter}{letter}"))
                {
                    result += number;

                    if(number < 0)
                    {
                        negativeNumbers.Add(number);
                    }

                    isNegative = false; 
                    number = 0;
                }
                else if(letter == '-')
                {
                    isNegative = true;
                }
                else if (char.IsDigit(letter))
                {
                    if (number >= 0)
                    {
                        number = number * 10 + (letter - '0');
                    }
                    else
                    {
                        number = number * 10 - (letter - '0');
                    }

                    if(isNegative && number != 0) 
                    {
                        number = 0 - number;
                        isNegative = false;
                    }
                }
                else
                {
                    number = 0;
                }

                previousletter = letter;
            }

            if(number <0)
            {
                negativeNumbers.Add(number);
            }

            if(negativeNumbers.Count != 0)
            {
                throw new ArgumentException(string.Join(",",negativeNumbers));
            }

            return result + number;
        }

        public bool IsDelimiter(string str)
        {
            return str[str.Length -1] == ',' || str == "\n";
        }
    }
}
