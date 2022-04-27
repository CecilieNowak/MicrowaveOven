using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Controllers
{
    public class CookController : ICookController
    {
        // Since there is a 2-way association, this cannot be set until the UI object has been created
        // It also demonstrates property dependency injection
        public IUserInterface UI { set; private get; }

        private bool isCooking = false;

        private IDisplay myDisplay;
        private IPowerTube myPowerTube;
        private ITimer myTimer;

        public CookController(
            ITimer timer,
            IDisplay display,
            IPowerTube powerTube,
            IUserInterface ui) : this(timer, display, powerTube)
        {
            UI = ui;
        }

        public CookController(
            ITimer timer,
            IDisplay display,
            IPowerTube powerTube)
        {
            myTimer = timer;
            myDisplay = display;
            myPowerTube = powerTube;

            timer.Expired += new EventHandler(OnTimerExpired);  
            //OnTimeExpired er knyttet til Expired-eventhandleren fra Timer-klassen
            timer.TimerTick += new EventHandler(OnTimerTick);
            //OnTimerTick er knyttet til TimerTick-eventhandleren fra Timer-klassen
        }

        public void StartCooking(int power, int time)
        {
            myPowerTube.TurnOn(power);
            myTimer.Start(time);
            //Når ovnen starter, så starter timer også
            isCooking = true;
        }

        public void Stop()
        {
            isCooking = false;
            myPowerTube.TurnOff();
            myTimer.Stop();    
            //Når ovnen stopper, så stopper timere også
        }

        //Når tiden er gået, så stopper ovnen med at varme maden, og powertube slukker. Og der udskrives en besked.
        public void OnTimerExpired(object sender, EventArgs e)
        {
            if (isCooking)
            {
                isCooking = false;
                myPowerTube.TurnOff();
                UI.CookingIsDone();
            }
        }

        //Når maden tilberedes og tiden går, bliver resterende tid udskrives.
        public void OnTimerTick(object sender, EventArgs e) //Event
        {
            if (isCooking)
            {
                //Den resterende tid fås fra Timer-klassen
                int remaining = myTimer.TimeRemaining; // + myTimer.ChangeCookingTime;
                myDisplay.ShowTime(remaining / 60, remaining % 60);
            }
        }

         public void IncreaseTime()
         {
             myTimer.IncreaseRemainingTime();
         }

        public void DecreaseTime()
        {
            myTimer.DecreaseRemainingTime();

            if (myTimer.TimeRemaining <= 0)
            {
                UI.TimeIsChangedToZero();
            }
        }
    }
}