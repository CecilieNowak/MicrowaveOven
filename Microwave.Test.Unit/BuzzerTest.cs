using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class BuzzerTest
    {
        private Buzzer uut;
        private IOutput output;
        
        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();

            uut = new Buzzer(output);
        }

        [Test]
        public void BuzzerTurnOn_WasOff_CorrectOutput()
        {
            uut.TurnOn();
            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Bib bib bib")));
        }

        [Test]
        public void BuzzerTurnOff_WasOn_CorrectOutput()
        {
            uut.TurnOn();
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void BuzzerTurnOn_WasOn_CorrectOutput()
        {
            uut.TurnOn();
            uut.TurnOn();
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Bib bib bib")));
        }

        [Test]
        public void BuzzerTurnOff_WasOff_CorrectOutput()
        {
            uut.TurnOff();
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }


    }
}
