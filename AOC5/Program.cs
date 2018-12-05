using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC5
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
                while ((line = sr.ReadLine()) != null)
                {
                    //Part one
                    while (Reduce(ref line) > 0);
                    Console.WriteLine("Answer 1: " + line.Length);

                    //Part two
                    int bestLength = int.MaxValue;
                    char bestChar = '0';
                    for(char c = 'A'; c <= 'Z'; ++c)
                    {
                        var lineCopy = line;
                        Remove(ref lineCopy, c);
                        while (Reduce(ref lineCopy) > 0);
                        if(bestLength > lineCopy.Length )
                        {
                            bestChar = c;
                            bestLength = lineCopy.Length;
                        }
                    }
                    Console.WriteLine("Answer 2: " + bestChar + ": " + bestLength);
                }
                Console.ReadLine();
            }
        }
        private static int Remove(ref string line, char c)
        {
            int removals = 0;
            for (int i = 0; i < line.Length - 1; ++i)
            {
                if (line[i] == c || line[i] - 32 == c)
                {
                    line = line.Remove(i, 1);
                    i--;
                    removals++;
                }
            }
            return removals;
        }
        private static int Reduce(ref string line)
        {
            int removals = 0;
            for(int i=0; i < line.Length-1; ++i)
            {
                if (line[i] == line[i+1] - 32 || line[i] - 32 == line[i + 1])
                {
                    line = line.Remove(i, 2);
                    i--;
                    removals++;
                }
            }
            return removals;
        }
    }
}
