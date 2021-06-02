using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Imitation
{
    class Model
    {
        public double current_time;
        public int events;
        public double l1,l2;
        public List<double> Statistic = new List<double>();
        public List<double> StatisticTime = new List<double>();
        public double[] StatisticFreq;
        public double[] StatisticCount;


        public ArrivalProcess arrivalProcess1;
        public ArrivalProcess arrivalProcess2;


        public Model(double intensity1, double intensity2)
        {
            l1 = intensity1;
            l2 = intensity2;
            arrivalProcess1 = new ArrivalProcess(intensity1);
            arrivalProcess2 = new ArrivalProcess(intensity2);

            current_time = 0;
            events = 0;
            arrivalProcess1.calculateTime(current_time);
            arrivalProcess2.calculateTime(current_time);
        }
        public double KolmogorovTest()
        {
            double sup = 0;
            for (int i = 0; i < StatisticCount.Length; i++)
            {
                double theoretical = PoissonDistribution(l1+l2,i);
                double empiric = StatisticCount[i] / current_time;
                double dif = Math.Abs(empiric - theoretical);
                if (dif > sup) sup = dif;
            }
            return sup;
        }

        public double calcAverage()
        {
            return Statistic.Count / current_time;
        }
        public int Factorial(int k)
        {
            int ans = 1;
            for (int i = 1; i <= k; i++)
            {
                ans *= i;
            }
            return ans;
        }
        public double PoissonDistribution(double lambda, int k)
        {
            double x = Math.Exp(-lambda) * Math.Pow(lambda, k) / Factorial(k);
            return x;
        }
        public void simulate(int max)
        {
            while (events < max)
            {
                events++;
                double ta1 = arrivalProcess1.get_ta();
                double ta2 = arrivalProcess2.get_ta();

                double min_time = Math.Min(ta1, ta2);
                //double dt = min_time - current_time;
                //StatisticTime.Add(dt);
                if (min_time == ta1) arrivalProcess1.calculateTime(min_time);
                if (min_time == ta2) arrivalProcess2.calculateTime(min_time);
                Statistic.Add(min_time);
                current_time = min_time;                   
            }

            int n2 = (int)current_time + 1;
            StatisticFreq = new double[n2];
            for (int i = 0; i < Statistic.Count; i++)
            {
                int j = (int)Statistic[i];
                StatisticFreq[j]++;
            }

            int n1 = (int)StatisticFreq.Max() + 1;
            StatisticCount = new double[n1];
            for (int i = 0; i < StatisticFreq.Length; i++)
            {
                int j = (int)StatisticFreq[i];
                StatisticCount[j]++;
            }
        }
    }
}
