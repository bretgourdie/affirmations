using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Affirmations
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            new Affirmation();

            var autoEvent = new AutoResetEvent(false);
            autoEvent.WaitOne();
        }
    }
}
