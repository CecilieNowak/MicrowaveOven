﻿using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;

        private bool IsOn = false;
        public int MaxPower { get; set; }

        public PowerTube(IOutput output, IConfiguration configuration)
        {
            myOutput = output;
            MaxPower = maxPower;
        }

        public void TurnOn(int power)
        {
            if (power < 1 || MaxPower < power)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and" + MaxPower.ToString());
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }

            myOutput.OutputLine($"PowerTube works with {power}");
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }
    }
}