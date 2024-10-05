using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strictly_come_coding
{
    public class WriteToFile
    {
        public void TextWrite(List<string> outputs, string filename = "result.txt")
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            File.WriteAllLines(filename, outputs);

            //using (TextWriter tw = new StreamWriter(filename))
            //{
            //    foreach (String s in outputs)
            //        tw.WriteLine(s);
            //}
        }
    }
}
