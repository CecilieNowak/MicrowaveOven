using System;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NuGet.Frameworks;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class CookControllerTest
    {
        private CookController uut;

        private IUserInterface ui;
        private ITimer timer;
        private IDisplay display;
        private IPowerTube powerTube;

        [SetUp]
        public void Setup()
        {
            ui = Substitute.For<IUserInterface>();
            timer = Substitute.For<ITimer>();
            display = Substitute.For<IDisplay>();
            powerTube = Substitute.For<IPowerTube>();

            uut = new CookController(timer, display, powerTube, ui);
        }

        [Test]
        public void StartCooking_ValidParameters_TimerStarted()
        {
            uut.StartCooking(50, 60);

            timer.Received().Start(60);
        }

        [Test]
        public void StartCooking_ValidParameters_PowerTubeStarted()
        {
            uut.StartCooking(50, 60);

            powerTube.Received().TurnOn(50);
        }

        [Test]
        public void Cooking_TimerTick_DisplayCalled()
        {
            uut.StartCooking(50, 60);

            timer.TimeRemaining.Returns(115);
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            display.Received().ShowTime(1, 55);
        }

        [Test]
        public void Cooking_TimerExpired_PowerTubeOff()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            powerTube.Received().TurnOff();
        }

        [Test]
        public void Cooking_TimerExpired_UICalled()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            ui.Received().CookingIsDone();
        }

        [Test]
        public void Cooking_Stop_PowerTubeOff()
        {
            uut.StartCooking(50, 60);
            uut.Stop();

            powerTube.Received().TurnOff();
        }
        //---

        [Test]
        public void CookController_IncreaseTime_TimerIsCalled()
        {
            uut.IncreaseTime();

            timer.Received(1).IncreaseRemainingTime();
        }

        [Test]
        public void CookController_DecreaseTime_TimerIsCalled()
        {
            uut.DecreaseTime(); 
            
            timer.Received(1).DecreaseRemainingTime();
        }

        [Test]
        public void CookController_ChangeTimeToZero_UIIsCalled()
        { 
            timer.TimeRemaining.Returns(0);
            
            uut.DecreaseTime();    

            timer.Received(1).DecreaseRemainingTime();
            ui.Received(1).TimeIsChangedToZero();
        }

        [Test]
        public void CookController_ChangeTimeToNegative1_UIIsCalled()
        {
            timer.TimeRemaining.Returns(-1);
            uut.DecreaseTime();
            ui.Received(1).TimeIsChangedToZero();
        }

        [Test]
        public void CookController_ChangeTimeToOne_UIIsNotCalled()
        {
            timer.TimeRemaining.Returns(1);
            uut.DecreaseTime();
            ui.Received(0).TimeIsChangedToZero();
        }
    }
}