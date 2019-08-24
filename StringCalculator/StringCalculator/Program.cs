using System;

namespace StringCalculator
{
    class Program
    {
        public static void Main(string[] args)
        {
            string val;
            Console.Write("Enter String: ");
            val = Console.ReadLine();

            ICalculator calculator = new StringCalculator();
            calculator.Calculate(val);
        }
    }
}
