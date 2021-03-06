using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class PowerTubeTest
    {
        private PowerTube uut;
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
           
            uut = new PowerTube(output, 700);
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(699)]
        [TestCase(700)]
        public void TurnOn_WasOffCorrectPower_CorrectOutput(int power)
        {
            
            uut.TurnOn(power);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"{power}")));
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(500)]
        [TestCase(700)]
        [TestCase(800)]
        [TestCase(899)]
        public void TurnOnMaxPowerIs900_WasOffCorrectPower_CorrectOutput(int power)
        {
            uut.MaxPower = 900;

            uut.TurnOn(power);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"{power}")));
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(200)]
        [TestCase(300)]
        [TestCase(499)]
        public void TurnOnMaxPowerIs500_WasOffCorrectPower_CorrectOutput(int power)
        {
            uut.MaxPower = 500;

            uut.TurnOn(power);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"{power}")));
        }


        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(701)]
        [TestCase(750)]
        public void TurnOn_WasOffOutOfRangePower_ThrowsException(int power)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.TurnOn(power));
        }

        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(901)]
        [TestCase(950)]
        public void TurnOnMaxPowerIs900_WasOffOutOfRangePower_ThrowsException(int power)
        {
            uut.MaxPower = 900;
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.TurnOn(power));
        }

        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(501)]
        [TestCase(550)]
        public void TurnOnMaxPowerIs500_WasOffOutOfRangePower_ThrowsException(int power)
        {
            uut.MaxPower = 500;
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.TurnOn(power));
        }

        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            uut.TurnOn(50);
            uut.TurnOff();
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }


        [Test]
        public void TurnOff_WasOff_NoOutput()
        {
            uut.TurnOff();
            output.DidNotReceive().OutputLine(Arg.Any<string>());
        }

        [Test]
        public void TurnOn_WasOn_ThrowsException()
        {
            
            uut.TurnOn(50);
            Assert.Throws<System.ApplicationException>(() => uut.TurnOn(60));
        }
    }
}