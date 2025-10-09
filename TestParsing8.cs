using csFastFloat;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Compression;
using System.IO.Pipelines;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace strictly_come_coding
{
    public static class TestParsing8
    {

        private static ReadOnlySpan<byte> NewLine => "\r\n"u8;
        private const byte Semicolon = (byte)';';

        public static async Task<IReadOnlyList<string>> ParseAsync(
            string inputFile,
            CancellationToken cancellationToken = default)
        {
            var byCity = new Dictionary<string, Measurement>(StringComparer.Ordinal);

            await using var fileStream = File.OpenRead(inputFile);
            var reader = PipeReader.Create(fileStream);

            var position = 0; // kept if you need external progress reporting

            while (true)
            {
                var readResult = await reader.ReadAsync(cancellationToken);
                var buffer = readResult.Buffer;

                var consumed = ParseBufferInto(byCity, ref buffer, ref position);

                // Tell the PipeReader what we’ve consumed & what we’ve examined.
                reader.AdvanceTo(consumed, buffer.End);

                if (readResult.IsCompleted) break;
            }

            await reader.CompleteAsync();

            // Materialise in sorted order: city=min/avg/max/count
            return byCity
                .OrderBy(kv => kv.Key, StringComparer.Ordinal)
                .Select(kv => $"{kv.Key}={kv.Value}")
                .ToList();
        }

        // Parses as many complete lines as available and updates the dictionary.
        private static SequencePosition ParseBufferInto(
            Dictionary<string, Measurement> store,
            ref ReadOnlySequence<byte> buffer,
            ref int position)
        {
            var seqReader = new SequenceReader<byte>(buffer);

            while (!seqReader.End)
            {
                if (!seqReader.TryReadToAny(out ReadOnlySpan<byte> line, NewLine, advancePastDelimiter: true))
                    break; // need more data

                position++; // optional: track lines processed

                if (TryParseMeasurement(line, out var city, out var temp))
                {
                    UpsertMeasurement(store, city, temp);
                }
                // else: skip malformed line silently (or log)
            }

            return seqReader.Position;
        }

        // Parse "City;12.3" from a single line (no allocations beyond the city string).
        private static bool TryParseMeasurement(
            ReadOnlySpan<byte> line,
            out string city,
            out float temperature)
        {
            var idx = line.IndexOf(Semicolon);
            if (idx <= 0 || idx >= line.Length - 1)
            {
                city = "";
                temperature = default;
                return false;
            }

            city = Encoding.UTF8.GetString(line[..idx]);
            temperature = FastFloatParser.ParseFloat(line[(idx + 1)..]);
            return true;
        }

        // Update measurement with clear, conventional code.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void UpsertMeasurement(
            Dictionary<string, Measurement> store,
            string city,
            float temperature)
        {
            if (store.TryGetValue(city, out var m))
            {
                store[city] = m.Update(temperature);
            }
            else
            {
                store.Add(city, new Measurement(temperature));
            }
        }

    }
}
