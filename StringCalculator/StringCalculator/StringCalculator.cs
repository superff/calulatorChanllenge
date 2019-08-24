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
            for(int i = 0; i < stringInput.Length; i++)
            {
                char letter = stringInput[i];

                if(letter == ',')
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
            }

            return result + number;
        }
    }
}
