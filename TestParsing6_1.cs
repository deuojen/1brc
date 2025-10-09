using csFastFloat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace strictly_come_coding
{
    public static class TestParsing6_1
    {
        // Prefer PascalCase for public members in .NET
        public static List<string> Solution(string inputFile)
        {
            var byCity = new Dictionary<string, Measurement>(StringComparer.Ordinal);

            foreach (var line in File.ReadLines(inputFile))
            {
                if (string.IsNullOrEmpty(line)) continue;

                // Parse "City;12.3"
                var span = line.AsSpan();
                var idx = span.IndexOf(';');
                if (idx <= 0 || idx >= span.Length - 1) continue; // malformed

                // Slice the original string once (avoids extra ToString on a Span)
                string city = line.Substring(0, idx);
                float temp = FastFloatParser.ParseFloat(span[(idx + 1)..]);

                // Clear, conventional upsert
                if (byCity.TryGetValue(city, out var m))
                {
                    byCity[city] = m.Update(temp);
                }
                else
                {
                    byCity.Add(city, new Measurement(temp));
                }
            }

            // Deterministic ordering
            return byCity
                .OrderBy(kv => kv.Key, StringComparer.Ordinal)
                .Select(kv => $"{kv.Key}={kv.Value}")
                .ToList();
        }
    }
}
