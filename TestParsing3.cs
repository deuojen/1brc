using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace strictly_come_coding
{
    public class TestParsing3
    {
        public List<string> solution(string inputFile)
        {
            var result = new List<string>();

            try
            {
                //var location = new Location("Abha");

                var _items = new List<string>();

                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(inputFile))
                {
                    string? line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine() ?? null) != null)
                    {
                        _items.Add(line);
                    }
                }

                var currentTemps = new List<float>();
                var currentCity = "";
                var asSpan = CollectionsMarshal.AsSpan(_items.OrderBy(x => x).ToList());
                for (int i = 0; i < asSpan.Length; i++)
                {

                    if(i % 10000 == 0)
                    {
                        Console.WriteLine(i.ToString());
                    }

                    var item = asSpan[i];
                    var split = item.Split(';');
                    var name = split[0];
                    var temp = float.Parse(split[1]);


                    if (string.IsNullOrEmpty(currentCity))
                    {
                        currentCity = name;
                        currentTemps.Add(temp);
                    }
                    else if (currentCity == name)
                    {
                        currentTemps.Add(temp);
                    }
                    else
                    {
                        var mean = currentTemps.Sum() / currentTemps.Count;

                        result.Add($"{currentCity}={currentTemps.Min()};{mean.ToString("N1")};{currentTemps.Max()}");

                        currentCity = name;
                        currentTemps = new List<float> { temp };
                    }

                }
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
