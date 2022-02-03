using System;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            int yesCounter = 4;
            int noCounter = 2;
            int maybeCounter = 3;

            int total = yesCounter + noCounter + maybeCounter;

            double yesPercent = CalculatePercentage(yesCounter, total);
            var noPercent = CalculatePercentage(noCounter, total); 
            var maybePercent = CalculatePercentage(maybeCounter, total);

            var excess = Math.Round(100 - yesPercent - noPercent - maybePercent, 2);

            Console.WriteLine($"Excess: {excess}");

            Console.WriteLine($"Yes Counts: {yesCounter}, Percentage: {yesPercent}%");
            Console.WriteLine($"No Counts: {noCounter}, Percentage: {noPercent}%");
            Console.WriteLine($"Maybe Counts: {maybeCounter}, Percentage: {maybePercent}%");

            Console.WriteLine($"Total Percentage: {yesPercent - noPercent - maybePercent}");


            if (AGreaterThanB(yesCounter, noCounter))
            {
                if (AGreaterThanB(yesCounter, maybeCounter))
                {
                    Console.WriteLine($"Yes won"); 
                }
                else if (ALessThanB(yesCounter, maybeCounter))
                {
                Console.WriteLine($"Maybe won");
                }
                else
                {
                    Console.WriteLine("$DRAW");
                }
            }
            else if (ALessThanB(yesCounter, noCounter))
            {
                if (AGreaterThanB(noCounter, maybeCounter))
                {
                    Console.WriteLine($"No won"); 
                }
                else if (ALessThanB(noCounter, maybeCounter))
                {
                Console.WriteLine($"Maybe won");
                }
                else
                {
                   Console.WriteLine("$DRAW"); 
                }
            }
            else if (maybeCounter > yesCounter)
            {
                Console.WriteLine($"Maybe won");
            }
            else 
            {
                Console.WriteLine("$DRAW");
            }
        }

        private static double CalculatePercentage(int yesCounter, int total)
        {
            return Math.Round(yesCounter * 100.0 / total, 2);
        }

        private static bool AGreaterThanB(int yesCounter, int noCounter)
        {
            return yesCounter > noCounter;
        }

        private static bool ALessThanB(int yesCounter, int noCounter)
        {
            return yesCounter < noCounter;
        }
    }
}
