using System;
using System.Collections.Generic;
using System.IO;

namespace AOC1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int bufSize = 9068;
            Stream inStream = Console.OpenStandardInput(bufSize);
            Console.SetIn(new StreamReader(inStream, Console.InputEncoding, false, bufSize));

            do
            {
                HashSet<int> seen = new HashSet<int>();
                var toSplit = Console.ReadLine();
                var operations = toSplit.Split(", ");
                int val = 0;
                bool foundTwice = false;
                seen.Add(val);
                while (!foundTwice)
                {
                    foreach (var operation in operations)
                    {
                        if (operation.StartsWith('+'))
                        {
                            //add
                            var valToAdd = int.Parse(operation.Substring(1));
                            val += valToAdd;
                        }
                        else if (operation.StartsWith('-'))
                        {
                            //subtract
                            var valToSubtract = int.Parse(operation.Substring(1));
                            val -= valToSubtract;
                        }
                        else
                            Console.WriteLine("Unknown operation" + operation);

                        if (seen.Contains(val))
                        {
                            Console.WriteLine("Seen twice" + val);
                            foundTwice = true;
                            break;
                        }
                        seen.Add(val);
                    }
                }
                Console.WriteLine("Total:" + val);

            }
            while (true);
        }
    }
}
