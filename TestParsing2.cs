using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strictly_come_coding
{
    public class TestParsing2
    {
        public List<string> solution(string inputFile)
        {
            var result = new List<string>();

            try
            {
                var dict = new Dictionary<string, Location>();
                var nameSet = new SortedSet<string>();

                var allLines = new string[1000000000];

                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = File.OpenText(inputFile))
                {

                    int x = 0;
                    while (!sr.EndOfStream)
                    {
                        allLines[x] = sr.ReadLine() ?? "";
                        x += 1;
                    }
                }

                Parallel.For(0, allLines.Length, x =>
                {
                    var split = allLines[x].Split(';');
                    var name = split[0];
                    var temp = float.Parse(split[1]);

                    if (dict.ContainsKey(name))
                    {
                        dict[name].Temps.Add(temp);
                    }
                    else
                    {
                        var location = new Location(name);
                        location.Temps.Add(temp);

                        dict.Add(name, location);
                    }
                });

                //Console.WriteLine($"{location.City}={location.GetMin()}/{location.GetMean().ToString("N1")}/{location.GetMax()}");

                result = dict.OrderBy(x => x.Key).Select(x => x.Value.ToString()).ToList();

            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return result;

        }
    }
}
