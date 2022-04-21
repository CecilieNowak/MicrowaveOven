using System;
using System.Collections.Generic;
using System.Text;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Buzzer : IBuzzer
    {
        private IOutput Output;
        

        public Buzzer(IOutput output)
        {
            Output = output;
        }
        public void TurnOn()
        {

            Output.OutputLine("Bib bib bib");

        }

        public void TurnOff()
        {
            Output.OutputLine("off");
        }
    }
}
