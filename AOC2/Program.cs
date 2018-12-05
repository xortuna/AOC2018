using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter file:");
            string inputFile = Console.ReadLine();
            using (StreamReader sr = new StreamReader(inputFile))
            {
                string line;
                int twos = 0;
                int threes = 0;
                List<string> seen = new List<string>();
                while ((line = sr.ReadLine()) != null)
                {
                    Dictionary<char, int> counts = new Dictionary<char, int>();
                    foreach(var c in line)
                    {
                        if (!counts.ContainsKey(c))
                            counts.Add(c, 0);
                        counts[c] += 1;
                    }
                    bool foundTwo = false;
                    bool foundThree = false;
                    foreach(var count in counts)
                    {
                        if (count.Value == 2 && !foundTwo)
                        {
                            twos++;
                            foundTwo = true;
                        }
                        if (count.Value == 3 && !foundThree)
                        {
                            threes++;
                            foundThree = true;
                        }
                    }


                    foreach(var test in seen)
                    {
                        if(line.Length == test.Length)
                        {
                            var matching = 0;
                            for (int i=0; i < line.Length; ++i)
                            {
                                if (line[i] == test[i])
                                    matching += 1;
                            }
                            if (matching == line.Length - 1)
                            {
                                Console.WriteLine(line + " VS " + test);
                            }

                        }
                    }
                    seen.Add(line);

                }
                Console.WriteLine("Twos: " + twos + " Threes: " + threes);
                Console.WriteLine("Answer: " + twos*threes);
                Console.Read();
            }
        }
    }
}
