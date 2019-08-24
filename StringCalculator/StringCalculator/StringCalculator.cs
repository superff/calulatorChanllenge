using System;
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
            for(int i = 0; i < stringInput.Length; i++)
            {
                char letter = stringInput[i];

                if(IsDelimiter($"{previousletter}{letter}"))
                {
                    result += number;
                    number = 0;
                }
                if (char.IsDigit(letter))
                {
                    number = number * 10 + (letter - '0');
                }
                else
                {
                    number = 0;
                }

                previousletter = letter;
            }

            return result + number;
        }

        public bool IsDelimiter(string str)
        {
            return str[str.Length -1] == ',' || str == "\n";
        }
    }
}
