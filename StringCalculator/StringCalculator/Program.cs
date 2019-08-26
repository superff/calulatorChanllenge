using System;
using System.Text;

namespace StringCalculator
{
    class Program
    {
        private static bool run = true;
        private static StringBuilder sb = new StringBuilder();
        private static ICalculator calculator = new StringCalculator();

        public static void Main(string[] args)
        {
            // Establish an event handler to process key press events.
            Console.CancelKeyPress += cancelHandler;
            Console.WriteLine("Enter String To Calulate");
            Console.WriteLine("press CTRL+C to end:");
            while (run)
            {
                sb.AppendLine(Console.ReadLine());
            }
        }

        protected static void cancelHandler(object sender, ConsoleCancelEventArgs args)
        {
            try
            {
                run = false;
                Console.WriteLine();
                // calculate the string input
                calculator.Calculate(sb.ToString());
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
        }
    }
}
