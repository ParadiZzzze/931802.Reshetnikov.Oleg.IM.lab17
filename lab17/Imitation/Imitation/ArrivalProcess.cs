using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imitation
{
    class ArrivalProcess
    {
        double Lambda;
        double arrival_time;
        public ArrivalProcess(double lambda)
        {
            Lambda = lambda;
            arrival_time = 1000;
        }
        public double get_ta()
        {
            return arrival_time;
        }

        public void calculateTime(double t)
        {
            Random rnd = new Random();
            double a = rnd.NextDouble();
            arrival_time = t - Math.Log(a)/Lambda;
        }
    }
}
