using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Blocks
{
    public class LimitBlock : BaseBlock
    {
        public double min { get; set; }
        public double max { get; set; }
        public LimitBlock(double min, double max) 
        {
            this.min = min;
            this.max = max;
        }
        public override double Calc(double x)
        {
            if (x > max) return max;
            if (x < min) return min;
            return x;
        }
    }
}
