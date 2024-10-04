using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strictly_come_coding
{
    public class TestParsingMethod2
    {
        public List<string> solution(string inputFile)
        {
            var result = new List<string>();

            try
            {
                var location = new Location("Abha");

                var lines = File.ReadAllLines(inputFile);
                var length = lines.Length;
                Console.WriteLine(length);

                for (int i = 0; i < length; i++)
                {
                    var split = lines[0].Split(';');

                    if (split[0] == "Abha")
                    {
                        location.Temps.Add(float.Parse(split[1]));
                        //Console.WriteLine(line);
                    }
                }

                Console.WriteLine($"{location.City}={location.GetMin()};{location.GetMean().ToString("N1")};{location.GetMax()}");
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
