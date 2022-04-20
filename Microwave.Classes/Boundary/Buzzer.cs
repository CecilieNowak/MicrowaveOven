using System;
using System.Collections.Generic;
using System.Text;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Buzzer : IBuzzer
    {
        private IOutput myOutput;
        

        public Buzzer(IOutput output)
        {
            myOutput = output;
        }
        public void TurnOn()
        {

            myOutput.OutputLine("Bib bib bib");

        }

        //public void TurnOff()
        //{

        //}
    }
}
