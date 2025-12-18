using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribbl.Net.Models
{
    public class Line
    {
        public double FromX { get; set; }
        public double FromY { get; set; }
        public double ToX { get; set; }
        public double ToY { get; set; }
        public string Color { get; set; } = null!;
        public int Thickness { get; set; }
    }
}
