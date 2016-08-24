using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affirmations
{
    public class Metrics
    {
        public bool IsPositive { get; protected set; }
        public double Milliseconds { get; protected set; }

        public Metrics(double milliseconds)
        {
            this.Milliseconds = milliseconds;
            calculateMetrics();
        }

        protected void calculateMetrics()
        {
            this.IsPositive = true;
        }
    }
}
