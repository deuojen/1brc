using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strictly_come_coding
{
    public class Location
    {
        public string City { get; set; }

        public List<float> Temps { get; set; } = [];

        public Location(string name)
        {
            City = name;
        }

        public float GetMin() {
            return Temps.Min();
        }

        public float GetMax()
        {
            return Temps.Max();
        }

        public float GetMean()
        {
            return Temps.Sum() / Temps.Count;
        }

        public override string ToString()
        {
            return $"{City}={GetMin()};{GetMean().ToString("N1")};{GetMax()}";
        }
    }
}
