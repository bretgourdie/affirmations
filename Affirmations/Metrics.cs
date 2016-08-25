using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Affirmations
{
    public class Metrics
    {
        public bool IsPositive { get; protected set; }
        public double Milliseconds { get; protected set; }
        public int KeyboardPresses { get; protected set; }
        public bool IsCalculated { get; protected set; }

        public Metrics(double milliseconds)
        {
            this.Milliseconds = milliseconds;
            this.IsCalculated = false;
            this.KeyboardPresses = 0;
        }

        [Conditional("DEBUG")]
        private void logToConsole(string message)
        {
            Console.WriteLine("DEBUG (Met): " + message);
        }

        public void CalculateMetrics()
        {
            this.IsPositive = determineIfPositive(
                this.KeyboardPresses, 
                this.Milliseconds);
            this.IsCalculated = true;

        }

        private bool determineIfPositive(int p1, double p2)
        {
            return false;
        }
    }
}
