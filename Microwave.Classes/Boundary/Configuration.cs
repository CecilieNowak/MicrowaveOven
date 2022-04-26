using System;
using System.Collections.Generic;
using System.Text;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Configuration : IConfiguration
    {
        public Configuration(int power)
        {
            MaxPower = power;
        }
        public int MaxPower { get; set; }
    }
}
