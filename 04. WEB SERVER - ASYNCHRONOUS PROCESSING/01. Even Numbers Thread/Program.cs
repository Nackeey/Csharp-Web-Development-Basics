namespace _01._Even_Numbers_Thread
{
    using System;
    using System.Threading;

    public class Program
    {
        public static void Main(string[] args)
        {
            Thread evens = new Thread(() => PrintEvenNumbers(1, 10000));
            evens.Start();

            while (true)
            {
                var line = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine(line);

                if (line == "exit")
                {
                    break; 
                }

            }

            evens.Join();
        }

        private static void PrintEvenNumbers(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                Thread.Sleep(1000);

                if (i % 2 == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
