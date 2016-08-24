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
        public int MouseClicks { get; protected set; }
        public int MouseDistance { get; protected set; }
        public int KeyboardPresses { get; protected set; }
        public bool IsCalculated { get; protected set; }

        protected MouseHook mouseHook { get; set; }
        protected KeyboardHook keyboardHook { get; set; }

        public Metrics(double milliseconds)
        {
            this.Milliseconds = milliseconds;
            this.IsCalculated = false;
            this.MouseClicks = 0;
            this.MouseDistance = 0;
            this.KeyboardPresses = 0;
            this.mouseHook = new MouseHook();
            this.keyboardHook = new KeyboardHook();
            listenForHooks();
        }

        protected void listenForHooks()
        {
            this.mouseHook.InstallHook();
            this.keyboardHook.InstallHook();

            this.mouseHook.MouseEvent += mouseHook_MouseEvent;
            this.keyboardHook.KeyboardEvent += keyboardHook_KeyboardEvent;
        }

        void keyboardHook_KeyboardEvent(KeyboardEvents kEvent, System.Windows.Forms.Keys key)
        {
            this.KeyboardPresses++;
        }

        void mouseHook_MouseEvent(MouseEvents mEvent, System.Drawing.Point point)
        {
            if (mEvent == MouseEvents.LeftButtonUp)
            {
                this.MouseClicks++;
            }
        }

        [Conditional("DEBUG")]
        private void logToConsole(string message)
        {
            Console.WriteLine("DEBUG (Met): " + message);
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
            if (this.mouseHook != null)
            {
                this.mouseHook.Dispose();
                this.mouseHook = null;
            }

            if (this.keyboardHook != null)
            {
                this.keyboardHook.Dispose();
                this.keyboardHook = null;
            }
        }
    }
}
