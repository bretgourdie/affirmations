using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Kennedy.ManagedHooks;

namespace Affirmations
{
    public class Metrics : IDisposable
    {
        public bool IsPositive { get; protected set; }
        public double Milliseconds { get; protected set; }
        public int KeyboardPresses { get; protected set; }
        public bool IsCalculated { get; protected set; }

        protected KeyboardHook keyboardHook { get; set; }

        public Metrics(double milliseconds)
        {
            this.Milliseconds = milliseconds;
            this.IsCalculated = false;
            this.KeyboardPresses = 0;
            this.keyboardHook = new KeyboardHook();
            listenForHooks();
        }

        protected void listenForHooks()
        {
            this.keyboardHook.InstallHook();

            this.keyboardHook.KeyboardEvent += keyboardHook_KeyboardEvent;
        }

        void keyboardHook_KeyboardEvent(KeyboardEvents kEvent, System.Windows.Forms.Keys key)
        {
            this.KeyboardPresses++;
        }


        [Conditional("DEBUG")]
        private void logToConsole(string message)
        {
            Console.WriteLine("DEBUG (Met): " + message);
        }

        public void CalculateMetrics()
        {
            this.KeyboardPresses = 1023;

            this.IsPositive = determineIfPositive(
                this.KeyboardPresses, 
                this.Milliseconds);
            this.IsCalculated = true;
        }

        private bool determineIfPositive(int p1, double p2)
        {
            return false;
        }

        public void Dispose()
        {
            if (this.keyboardHook != null)
            {
                this.keyboardHook.Dispose();
                this.keyboardHook = null;
            }
        }
    }
}
