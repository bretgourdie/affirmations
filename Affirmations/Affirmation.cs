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
        string processTitle = "SDI Periodic Encouragement Program";

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

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {



        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine(
                "In OnStart([\"" 
                + String.Join("\", \"", args) 
                + "\"])");

            setTimerInterval(this.timer);
        }

        protected override void OnStop()
        {
            Console.WriteLine("In OnEnd()");

            sayGoodBye();
        }

        private void sayStatus()
        {
            var thingToSay = new StringBuilder();

            thingToSay.Append(generateGreeting());

            thingToSay.Append(generateCondition());

            sayThing(thingToSay);
        }

        private void sayGoodBye()
        {
            var thingToSay = new StringBuilder();
            var condition = true;

            thingToSay.Append("Congratulations, ");
            thingToSay.Append(getFullName());
            thingToSay.Append("!");

            thingToSay.AppendLine();
            thingToSay.AppendLine();

            thingToSay.Append("We appreciate all of your ");
            var degreeOfWork = condition ? "efficient" : "hard";
            thingToSay.Append(degreeOfWork);

            thingToSay.Append(" work. Please stand by to be ");
            var employmentStatus = condition ? "promoted" : "fired";
            thingToSay.Append(employmentStatus);
            thingToSay.Append(".");

            sayThing(thingToSay);
        }

        private string getFullName()
        {
            return UserPrincipal.Current.DisplayName;
        }

        private void sayThing(StringBuilder thingToSay)
        {
            sayThing(thingToSay.ToString());
        }

        private void sayThing(string thingToSay)
        {
            global::System.Windows.Forms.MessageBox.Show(
                thingToSay,
                processTitle,
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Information,
                System.Windows.Forms.MessageBoxDefaultButton.Button1,
                System.Windows.Forms.MessageBoxOptions.ServiceNotification);
        }

        private void setTimerInterval(Timer timer)
        {
            var rand = new Random();

            var minutesToWait = rand.Next(1, 4);

            var secondsToWait = minutesToWait * 60;

            var millisecondsToWait = secondsToWait * 1000;

            timer.Interval = millisecondsToWait;
        }

        private StringBuilder generateGreeting()
        {
            var greeting = new StringBuilder();

            greeting.Append("Hello, ");

            var fullName = getFullName();
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
