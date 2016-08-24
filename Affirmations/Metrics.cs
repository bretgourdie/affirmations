using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affirmations
{
    public class Metrics : IDisposable
    {
        public bool IsPositive { get; protected set; }
        public double Milliseconds { get; protected set; }
        public int MouseClicks { get; protected set; }
        public int MouseDistance { get; protected set; }
        public int KeyboardPresses { get; protected set; }

        public bool IsCalculated { get; protected set; }

        public Metrics(double milliseconds)
        {
            this.Milliseconds = milliseconds;
            this.IsCalculated = false;
            this.MouseClicks = 0;
            this.MouseDistance = 0;
            this.KeyboardPresses = 0;
            listenForHooks();
        }

        protected void listenForHooks()
        {

        }

        public void CalculateMetrics()
        {
            this.MouseClicks = 452;
            this.KeyboardPresses = 1023;

            this.IsPositive = determineIfPositive(
                this.MouseClicks, 
                this.MouseDistance, 
                this.KeyboardPresses, 
                this.Milliseconds);
            this.IsCalculated = true;
        }

        private bool determineIfPositive(int p1, int p2, int p3, double p4)
        {
            return false;
        }

        public void Dispose()
        {

        }
    }
}
