﻿using System;
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

namespace Affirmations
{
    public partial class Affirmation 
    {
        Timer timer;
        string processTitle = "SDI Periodic Encouragement Program";

        public Affirmation()
        {
            initializeClass();
        }

        private void initializeClass()
        {
            this.timer = new Timer();

            this.timer.Elapsed += timer_Elapsed;

            setTimerInterval(this.timer);
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            logToConsole("Timer elapsed!");
            sayStatus();
            setTimerInterval(this.timer);
        }

        private void sayStatus()
        {
            var thingToSay = new StringBuilder();

            var greeting = generateGreeting();
            thingToSay.Append(greeting);

            thingToSay.AppendLine();
            thingToSay.AppendLine();

            var condition = generateCondition();
            thingToSay.Append(condition);

            logToConsole("Saying status \"" + thingToSay.ToString() + "\"");

            sayThing(thingToSay);
        }

        [Conditional("DEBUG")]
        private void logToConsole(string message)
        {
            Console.WriteLine("DEBUG: " + message);
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

            logToConsole("Saying goodbye \"" + thingToSay.ToString() + "\"");

            sayThing(thingToSay);
        }

        private string getFullName()
        {
            var searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
            var collection = searcher.Get();
            var name = (string)collection.Cast<ManagementBaseObject>().First()["UserName"];
            var displayName = name;

            // Going for the gold
            try
            {
                var directoryEntrySearch = "WinNT://" + name.Replace('\\', '/');
                logToConsole("Searching for \"" + directoryEntrySearch + "\"");
                var directoryEntry = new DirectoryEntry(directoryEntrySearch);
                displayName = directoryEntry.Properties["fullName"].Value.ToString();
                logToConsole("Name is \"" + displayName + "\"");
            }
            catch (NullReferenceException)
            {
                logToConsole("Name not found, using \"" + displayName + "\"");
            }

            return displayName;
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
