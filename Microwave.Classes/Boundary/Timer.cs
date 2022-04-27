using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Timer : ITimer
    {
        //Den restende tid gemmes i denne property, så den kan bruges i CookController-klassen
        public int TimeRemaining { get; private set; }
        public int ChangeTime { get; set; } 
        
        public event EventHandler Expired;
        public event EventHandler TimerTick;

        private System.Timers.Timer timer;

        public Timer()
        {
            timer = new System.Timers.Timer();
            // Bind OnTimerEvent with an object of this, and set up the event
            timer.Elapsed += OnTimerEvent;
            timer.Interval = 1000; // 1 second intervals
            timer.AutoReset = true;  // Repeatable timer

            ChangeTime = 30;
        }

        //Timer starter
        public void Start(int time)
        {
            TimeRemaining = time;
            timer.Enabled = true;
        }

        //Timer stopper
        public void Stop()
        {
            timer.Enabled = false;
        }

        //Når tiden er gået, får CookController (observer) besked om det 
        private void Expire()
        {
            timer.Enabled = false;
            Expired?.Invoke(this, System.EventArgs.Empty);
        }

        //Når 1 sekund er gået, får CookController (observer) besked om det
        private void OnTimerEvent(object sender, System.Timers.ElapsedEventArgs args)
        {
            // One tick has passed
            // Do what I should
            TimeRemaining -= 1; 

            TimerTick?.Invoke(this, EventArgs.Empty);

            //Hvis tiden er lig eller under 0, så kaldes Expire(), så CookController kan få besked
            if (TimeRemaining <= 0)
            {
                Expire();
            }
        }

        //øger tid med 1 minut
        public void IncreaseRemainingTime()
        {
            TimeRemaining += ChangeTime;
        }

        //Reducerer tid med 1 minut
        public void DecreaseRemainingTime()
        {
            TimeRemaining -= ChangeTime;
        }
    }
}