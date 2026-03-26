using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Blocks
{
    public class LimitedIntBlock : BaseBlock
    {
        private double prev = 0;
        private double sum = 0;
        private double dt, min, max;

        public double z
        {
            get => sum;
            set
            {
                sum = value;
                prev = 0;
            }
        }

        public LimitedIntBlock(double dt, double min, double max, double startZ)
        {
            this.dt = dt;
            this.min = min;
            this.max = max;
            this.sum = startZ;
        }

        public override double Calc(double x)
        {
            sum += (x + prev) * dt / 2;
            sum = (sum > max) ? max : (sum < min) ? min : sum;
            prev = x;
            return sum;
        }
    }
}
