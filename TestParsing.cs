﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strictly_come_coding
{
    public class TestParsing
    {
        public List<string> solution(string inputFile)
        {
            var result = new List<string>();

            try
            {
                //var location = new Location("Abha");

                var dict = new Dictionary<string, Location>();

                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(inputFile))
                {
                    string? line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine() ?? null) != null)
                    {
                        var split = line.Split(';');
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
                    }
                }

                //Console.WriteLine($"{location.City}={location.GetMin()}/{location.GetMean().ToString("N1")}/{location.GetMax()}");

                //result = dict.OrderBy(x => x.Key).Select(x => x.Value.ToString()).ToList();

                // test no sort
                result = dict.Select(x => x.Value.ToString()).ToList();

            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return result;
        }

        public void WriteOutput(List<string> outputs, string filename= "result.txt") {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            File.WriteAllLines(filename, outputs);
        }
    }
}
