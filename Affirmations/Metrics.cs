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
        public bool IsCalculated { get; protected set; }
        private Dictionary<Process, PerformanceCounter> cpuCounterDict { get; set; }
        private Dictionary<Process, List<double>> cpuTimeDict { get; set; }

        public Metrics(double milliseconds)
        {
            this.Milliseconds = milliseconds;
            initializePerformanceCounter();
            this.cpuTimeDict = new Dictionary<Process, List<double>>();
            this.IsCalculated = false;
        }

        protected void initializePerformanceCounter()
        {
            var processList = new List<Process>();

            var pProcess = Process.GetProcessesByName("prowin32.exe");

            var oProcess = Process.GetProcessesByName("order_entry_o0001.exe");

            processList.AddRange(pProcess);
            processList.AddRange(oProcess);

            this.cpuCounterDict = new Dictionary<Process, PerformanceCounter>();
            foreach (var proc in processList)
            {
                this.cpuCounterDict.Add(
                    proc,
                    new PerformanceCounter("Process", "% Processor Time", proc.ProcessName));
            }
        }

        public void Tick()
        {
            foreach (var pair in this.cpuCounterDict)
            {
                var cpu = pair.Value.NextValue();

                this.cpuTimeDict[pair.Key].Add(cpu);
            }
        }

        [Conditional("DEBUG")]
        private void logToConsole(string message)
        {
            Console.WriteLine("DEBUG (Met): " + message);
        }

        public void CalculateMetrics()
        {
            this.IsPositive = determineIfPositive(
                0,
                this.Milliseconds);
            this.IsCalculated = true;

        }

        private bool determineIfPositive(int p1, double p2)
        {
            return false;
        }
    }
}
