using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using System.Timers;
using System.DirectoryServices.AccountManagement;

namespace Affirmations
{
    public partial class Affirmation : ServiceBase
    {
        Timer timer;

        public Affirmation()
        {
            Console.WriteLine("In constructor");
            InitializeComponent();
            initializeClass();
        }

        private void initializeClass()
        {
            Console.WriteLine("In initializeClass()");
            timer = new Timer();

            timer.Elapsed += timer_Elapsed;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var thingToSay = new StringBuilder();

            thingToSay.Append(generateGreeting());

            thingToSay.Append(generateCondition());

            global::System.Windows.Forms.MessageBox.Show(
                thingToSay.ToString(),
                "SDI Periodic Encouragement Program",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Information,
                System.Windows.Forms.MessageBoxDefaultButton.Button1,
                System.Windows.Forms.MessageBoxOptions.ServiceNotification);
        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine(
                "In OnStart([\"" 
                + String.Join("\", \"", args) 
                + "\"])");

            var rand = new Random();

            timer.Interval = rand.Next(60000, 150000);
        }

        protected override void OnStop()
        {
            Console.WriteLine("In OnEnd()");
        }

        private StringBuilder generateGreeting()
        {
            var greeting = new StringBuilder();

            greeting.Append("Hello, ");

            var fullName = UserPrincipal.Current.DisplayName;
            greeting.Append(fullName);

            greeting.Append("!");
            greeting.AppendLine();
            greeting.AppendLine();

            return greeting;
        }

        private StringBuilder generateCondition()
        {
            var conditionSaying = new StringBuilder();
            var condition = true;

            conditionSaying.Append("You've been doing ");
            
            var curCondition = condition ? "well" : "ok";
            
            conditionSaying.Append(curCondition);

            conditionSaying.Append(" today.");

            conditionSaying.AppendLine();
            conditionSaying.AppendLine();

            var followupMessage = 
                condition ? "Keep up the good work!" : "Keep working hard!";

            return conditionSaying;
        }
    }
}
