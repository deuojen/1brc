//using BenchmarkDotNet.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace strictly_come_coding
//{
//    [MemoryDiagnoser]
//    public class SplitLinesBenchmark
//    {
//        private const string Data = "Nickname: meziantou\r\nFirstName: Gérald\nLastName: Barré";
//        private readonly char[] Separators = new[] { '\r', '\n' };

//        [Benchmark]
//        public void StringReader()
//        {
//            var reader = new StringReader(Data);
//            string line;
//            while ((line = reader.ReadLine()) != null)
//            {
//            }
//        }

//        [Benchmark]
//        public void Split()
//        {
//            foreach (var line in Data.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
//            {
//            }
//        }

//        [Benchmark]
//        public void Split_Preallocated()
//        {
//            foreach (var line in Data.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
//            {
//            }
//        }

//        [Benchmark]
//        public void Span()
//        {
//            foreach (ReadOnlySpan<char> item in Data.SplitLines())
//            {
//            }
//        }
//    }
//}
