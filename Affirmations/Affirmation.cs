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
            logToEventLog("In constructor");
            InitializeComponent();
            initializeClass();
        }

        private void initializeClass()
        {
            logToEventLog("In initializeClass()");
            timer = new Timer();

            timer.Elapsed += timer_Elapsed;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            logToEventLog("Timer elapsed!");
            sayStatus();
        }

        protected override void OnStart(string[] args)
        {
            logToEventLog(
                "In OnStart([\"" 
                + String.Join("\", \"", args) 
                + "\"])");

            setTimerInterval(this.timer);
        }

        protected override void OnStop()
        {
            logToEventLog("In OnEnd()");

            sayGoodBye();
        }

        private void sayStatus()
        {
            var thingToSay = new StringBuilder();

            thingToSay.Append(generateGreeting());

            thingToSay.AppendLine();
            thingToSay.AppendLine();

            thingToSay.Append(generateCondition());

            logToEventLog("Saying status \"" + thingToSay.ToString() + "\"");

            sayThing(thingToSay);
        }

        [Conditional("DEBUG")]
        private void logToEventLog(string message, string log, EventLogEntryType entryType)
        {
            var source = "SDI Periodic Encouragement Service";

            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, log);
            }

            EventLog.WriteEntry(source, message, entryType);
        }

        [Conditional("DEBUG")]
        private void logToEventLog(string message)
        {
            logToEventLog(message, "Application", EventLogEntryType.Information);
        }

        [Conditional("DEBUG")]
        private void logToEventLog(string message, EventLogEntryType entryType)
        {
            logToEventLog(message, "Application", entryType);
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

            logToEventLog("Saying goodbye \"" + thingToSay.ToString() + "\"");

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

            timer.Start();

            logToEventLog("Timer started with interval " + millisecondsToWait.ToString());
        }

        private StringBuilder generateGreeting()
        {
            var greeting = new StringBuilder();

            greeting.Append("Hello, ");

            var fullName = getFullName();
            greeting.Append(fullName);

            greeting.Append("!");
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
