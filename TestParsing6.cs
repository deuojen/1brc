using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace strictly_come_coding
{
    public class TestParsing6
    {
        public List<string> solution(string inputFile)
        {
            var result = new List<string>();

            try
            {
                Console.WriteLine("{0} parsing starting.", DateTime.Now);

                var dict = new Dictionary<string, Measurement>();

                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(inputFile))
                {
                    int index = 0;
                    // row count 1_000_000_000
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while (!sr.EndOfStream)
                    {

                        if (index % 100_000_000 == 0)
                        {
                            Console.WriteLine("{0} - i: {1}", DateTime.Now, index);
                        }

                        var line = sr.ReadLine().AsSpan();
                        var splitIndex = line.IndexOf(';');
                        var city = line.Slice(0, splitIndex);
                        var tempFloat = float.Parse(line.Slice(splitIndex + 1));

                        ref var valOrNull = ref CollectionsMarshal.GetValueRefOrNullRef(dict, city.ToString());

                        if (!Unsafe.IsNullRef(ref valOrNull))
                        {
                            dict[city.ToString()].Add(tempFloat);
                        }
                        else
                        {
                            var measurement = new Measurement(tempFloat);
                            dict.Add(city.ToString(), measurement);
                        }

                        index++;
                    }
                }

                Console.WriteLine("{0} sorting starting.", DateTime.Now);
                result = dict.OrderBy(x => x.Key).Select(x => $"{x.Key}={x.Value.ToString()}").ToList();

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
