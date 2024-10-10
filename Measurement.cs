using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strictly_come_coding
{
    public struct Measurement
    {
        public string Name;

        public float Min;

        public float Total;

        public float Max;

        public float Count;

        public float Mean;

        public Measurement(float value)
        {
            Min = value;
            Total = value;
            Max = value;
            Count = 1;
            Mean = 0;
            Name = string.Empty;
        }

        public Measurement(string name, float value)
        {
            Min = value;
            Total = value;
            Max = value;
            Count = 1;
            Mean = 0;
            Name = name;
        }

        public Measurement Update(float value)
        {
            if ( value < Min ) Min = value;

            if ( value > Max ) Max = value;

            Total += value;
            Count++;

            return this;
        }

        public override string ToString()
        {
            Mean = Total / Count;
            return $"{Min.ToString("N1")}/{Mean.ToString("N1")}/{Max.ToString("N1")}";
        }
    }
}
