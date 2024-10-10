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
    public static class TestParsing7
    {
        private static ReadOnlySpan<byte> NewLine => "\r\n"u8;
        public static async Task<List<string>> solution(string inputFile)
        {
            var result = new List<string>();

            try
            {
                //Console.WriteLine("{0} parsing starting.", DateTime.Now);

                var position = 0;

                var dict = new Dictionary<string, Measurement>();

                await using var fileStream = File.OpenRead(inputFile);
                //await using var decompressionStream = new GZipStream(fileStream, CompressionMode.Decompress);

                var pipeReader = PipeReader.Create(fileStream);

                // int index = 1;
                // row count 1_000_000_000

                while (true)
                {
                    var readResult = await pipeReader.ReadAsync();

                    var buffer = readResult.Buffer;

                    var sequencePosition = ParseLines(dict, ref buffer, ref position);

                    pipeReader.AdvanceTo(sequencePosition, buffer.End);

                    if (readResult.IsCompleted)
                    {
                        break;
                    }

                }

                pipeReader.Complete();

                //Console.WriteLine("{0} sorting starting.", DateTime.Now);
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

        private static SequencePosition ParseLines(Dictionary<string, Measurement> items, ref ReadOnlySequence<byte> buffer, ref int position)
        {
            var reader = new SequenceReader<byte>(buffer);

            while (!reader.End)
            {
                if (!reader.TryReadToAny(out ReadOnlySpan<byte> line, NewLine, true)) break;

                var parsedLine = LineParser.ParseLine(line);

                if (parsedLine.HasValue)
                {
                    ref var valOrNull = ref CollectionsMarshal.GetValueRefOrNullRef(items, parsedLine.Value.Name);

                    if (!Unsafe.IsNullRef(ref valOrNull))
                    {
                        items[parsedLine.Value.Name.ToString()] = valOrNull.Update(parsedLine.Value.Temp);
                    }
                    else
                    {
                        var measurement = new Measurement(parsedLine.Value.Temp);
                        items.Add(parsedLine.Value.Name.ToString(), measurement);
                    }
                }

            }

            return reader.Position;
        }

        private static class LineParser
        {
            private const byte SemiColon = (byte)';';

            public static MeasurementLineStruct? ParseLine(ReadOnlySpan<byte> line)
            {
                var splitIndex = line.IndexOf(SemiColon);
                
                var city = Encoding.UTF8.GetString(line.Slice(0, splitIndex));
                var tempFloat = FastFloatParser.ParseFloat(line.Slice(splitIndex + 1));

                return new MeasurementLineStruct(city, tempFloat);

            }
        }
    }
}
