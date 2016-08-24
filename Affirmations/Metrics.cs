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
        public int Milliseconds { get; protected set; }

        public Metrics(int milliseconds)
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
