using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC6
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
                }
            }
            Console.ReadLine();
        }
    }
}
