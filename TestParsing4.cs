using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strictly_come_coding
{
    public class TestParsing4
    {
        public List<string> solution(string inputFile)
        {
            var result = new List<string>();

            try
            {
                var dict = new Dictionary<string, Measurement>();
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(inputFile))
                {
                    int index = 1;
                    string? line;
                    // row count 1_000_000_000
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine() ?? null) != null)
                    {
                        if (index % 100_000_000 == 0)
                        {
                            Console.WriteLine("i: {0}, l: {1}", index, line);
                        }

                        var split = line.Split(';');
                        var name = split[0];
                        var temp = float.Parse(split[1]);

                        if (dict.ContainsKey(name))
                        {
                            dict[name].Update(temp);
                        }
                        else
                        {
                            var measurement = new Measurement(temp);
                            dict.Add(name, measurement);
                        }

                        index++;
                    }
                }

                //result = dict.OrderBy(x => x.Key).Select(x => $"{x.Key}={x.Value.ToString()}").ToList();

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
