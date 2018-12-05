using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AOC3
{
    class Program
    {
        class FabricCor
        {
            public int Count = 0;
            public int id = 0;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter file:");
            string inputFile = Console.ReadLine();
            int overlaps = 0;
            Regex parser = new Regex(@"#(\d+)+\s@\s(\d+),(\d+):\s(\d+)x(\d+)");
            Dictionary<int, bool> goodIdeas = new Dictionary<int, bool>();
            using (StreamReader sr = new StreamReader(inputFile))
            {
                string line;
                List<List<FabricCor>> area = new List<List<FabricCor>>();
                while ((line = sr.ReadLine()) != null)
                {
                    var match = parser.Match(line);
                    if(match.Success)
                    {
                        var id = int.Parse(match.Groups[1].Value);
                        var xPos = int.Parse(match.Groups[2].Value);
                        var yPos = int.Parse(match.Groups[3].Value);
                        var width = int.Parse(match.Groups[4].Value);
                        var height = int.Parse(match.Groups[5].Value);
                        for(int x = 0; x < width; ++x )
                        {
                            for(int y =0; y < height; ++y)
                            {
                                if (!goodIdeas.ContainsKey(id))
                                    goodIdeas.Add(id, true);
                                while (area.Count <= xPos + x)
                                    area.Add(new List<FabricCor>());
                                while (area[xPos+x].Count <= yPos + y)
                                    area[xPos+x].Add(new FabricCor());
                                if(area[xPos+x][yPos+y].Count >= 1)
                                {
                                    goodIdeas[area[xPos + x][yPos + y].id] = false;
                                    goodIdeas[id] = false;
                                }
                                area[xPos + x][yPos + y].Count++;
                                area[xPos + x][yPos + y].id = id;

                            }
                        }
                    }
                }
                foreach (var pos in area)
                {
                    foreach (var other in pos)
                    {
                        if (other.Count > 1)
                        {
                            overlaps++;
                        }
                    }
                }
                foreach (var idea in goodIdeas)
                {
                    if (idea.Value)
                        Console.WriteLine("No overlaps" + idea.Key);
                }

                Console.WriteLine("overlaps: " + overlaps);
                Console.ReadLine();
            }
        }
    }
}
