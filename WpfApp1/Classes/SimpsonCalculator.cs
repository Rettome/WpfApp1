﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WpfApp1.Classes
{
   public class SimpsonCalculator : ICalculator
    {
     public double Calculate(int splitCount, double upLim, double lowLim, Func<double, double> integral, out double time)
        {
            if (splitCount <= 0)
            {
                throw new ArgumentException();
            }
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var h = (upLim - lowLim) / (double)splitCount;
            var sum1 = 0d;
            var sum2 = 0d;
            for (var k = 1; k <= splitCount; k++)
            {
                var xk = lowLim + k * h;
                if (k <= splitCount - 1)
                {
                    sum1 += integral(xk);
                }

                var xk_1 = lowLim + (k - 1) * h;
                sum2 += integral((xk + xk_1) / 2);
            }

            var result = h / 3d * (1d / 2d * integral(lowLim) + sum1 + 2 * sum2 + 1d / 2d * integral(upLim));
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            time = ts.TotalMilliseconds;
            return result;
        }
    }
}
