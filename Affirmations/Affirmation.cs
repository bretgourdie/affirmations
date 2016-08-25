using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Timers;
using System.Management;
using System.DirectoryServices;
using swf = System.Windows.Forms;
using System.Drawing;
using System.DirectoryServices.AccountManagement;

namespace Affirmations
{
    public partial class Affirmation 
    {
        Timer timer;
        Metrics metrics;
        string processTitle = "SDI Periodic Encouragement Program";

        public Affirmation()
        {
            initializeClass();
        }

        public void Wait()
        {
            System.Threading.Thread.Yield();
        }

        private void initializeClass()
        {
            this.timer = new Timer();

            this.timer.Elapsed += timer_Elapsed;

            reloadComponents();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            logToConsole("Timer elapsed!");

            metrics.CalculateMetrics();
            sayStatus(this.metrics);

            reloadComponents();
        }

        private void reloadComponents()
        {
            setTimerInterval(this.timer);
            this.metrics = restartMetrics(this.timer.Interval);
        }

        private Metrics restartMetrics(double milliseconds)
        {
            var metrics = new Metrics(milliseconds);

            return metrics;
        }

        private void sayStatus(Metrics metrics)
        {
            var thingToSay = new StringBuilder();

            var greeting = generateGreeting();
            thingToSay.Append(greeting);

            thingToSay.AppendLine();

            var condition = generateCondition(metrics);
            thingToSay.Append(condition);

            logToConsole("Saying status \"" + thingToSay.ToString() + "\"");

            sayThing(thingToSay, metrics.IsPositive);
        }

        [Conditional("DEBUG")]
        private void logToConsole(string message)
        {
            Console.WriteLine("DEBUG: " + message);
        }

        private void sayGoodBye(bool isPositive)
        {
            var thingToSay = new StringBuilder();

            thingToSay.Append("Congratulations, ");
            thingToSay.Append(getFullName());
            thingToSay.Append("!");

            thingToSay.AppendLine();
            thingToSay.AppendLine();

            thingToSay.Append("We appreciate all of your ");
            var degreeOfWork = isPositive ? "efficient" : "hard";
            thingToSay.Append(degreeOfWork);

            thingToSay.Append(" work. Please stand by to be ");
            var employmentStatus = isPositive ? "promoted" : "fired";
            thingToSay.Append(employmentStatus);
            thingToSay.Append(".");

            logToConsole("Saying goodbye \"" + thingToSay.ToString() + "\"");

            sayThing(thingToSay, isPositive);
        }


        private string getFullName()
        {
            var displayName = UserPrincipal.Current.DisplayName;

            return displayName;
        }

        private void sayThing(StringBuilder thingToSay, bool isPositive)
        {
            sayThing(thingToSay.ToString(), isPositive);
        }

        private void sayThing(string thingToSay, bool isPositive)
        {
            var notifyIcon = System.Drawing.SystemIcons.Information;
            var balloonTipIcon = isPositive ? swf.ToolTipIcon.Info : swf.ToolTipIcon.Warning;

            using (var notification = new System.Windows.Forms.NotifyIcon())
            {
                notification.Visible = true;
                notification.Icon = notifyIcon;
                notification.BalloonTipTitle = this.processTitle;
                notification.BalloonTipText = thingToSay;

                notification.ShowBalloonTip(5);

                System.Threading.Thread.Sleep(10000);
            }
        }

        private void setTimerInterval(Timer timer)
        {
            var rand = new Random();

            var minutesToWait = rand.Next(1, 4);

            var secondsToWait = minutesToWait * 60;

            var millisecondsToWait = secondsToWait * 1000;

            timer.Interval = millisecondsToWait;

            timer.Start();

            logToConsole(
                "Timer started with interval " 
                + millisecondsToWait.ToString()
                + " ("
                + minutesToWait.ToString() 
                + " minutes)");
        }

        private StringBuilder generateGreeting()
        {
            var greeting = new StringBuilder();

            greeting.Append("Hello, ");

            var fullName = getFullName();
            var firstName = fullName;
            var splitName = fullName.Split(' ');
            if (splitName.Length > 0)
            {
                firstName = splitName[0];
            }
            greeting.Append(firstName);

            greeting.Append("!");
            return greeting;
        }

        private StringBuilder generateCondition(Metrics metrics)
        {
            var conditionSaying = new StringBuilder();

            conditionSaying.Append("You've been doing ");
            
            var curCondition = metrics.IsPositive ? "well" : "OK";
            
            conditionSaying.Append(curCondition);

            conditionSaying.AppendLine(" today.");

            conditionSaying.AppendLine("Keyboard Presses: " + metrics.KeyboardPresses);
            conditionSaying.AppendLine();

            var followupMessage = 
                metrics.IsPositive ? "Keep up the good work!" : "Please work harder.";

            conditionSaying.Append(followupMessage);

            return conditionSaying;
        }
    }
}
