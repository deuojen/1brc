using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strictly_come_coding
{
    public struct MeasurementLineStruct
    {
        public string Name;

        public float Temp;

        public MeasurementLineStruct(string name, float value)
        {
            Temp = value;
            Name = name;
        }
    }
}
