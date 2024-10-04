using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strictly_come_coding
{
    public struct Measurement
    {
        public float Min;

        public float Total;

        public float Max;

        public float Count;

        public Measurement(float value)
        {
            Min = value;
            Total = value;
            Max = value;
            Count = 0;
        }

        public void Add(float value)
        {
            if ( value < Min ) Min = value;

            if ( value > Max ) Max = value;

            Total += value;
            Count++;
        }

        public override string ToString()
        {
            return $"{Min.ToString("N1")};{(Total/ Count).ToString("N1")};{Max.ToString("N1")}";
        }
    }
}
