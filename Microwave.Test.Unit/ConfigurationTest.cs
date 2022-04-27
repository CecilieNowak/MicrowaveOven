using System;
using System.Collections.Generic;
using System.Text;
using Microwave.Classes.Boundary;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    class ConfigurationTest
    {


        private Configuration uut;

        [SetUp]
        public void Setup()
        {
            uut = new Configuration(0); //default value
        }

        [Test]
        public void SetMaxPower_ValueIsSetToExpectedValue()
        {
            uut.MaxPower = 100;
            Assert.That(uut.MaxPower, Is.EqualTo(100));


        }

        [Test]
        public void GetMaxPower_ReturnsExpectedValue()
        {
            uut.MaxPower = 100;
            int value = uut.MaxPower;
            Assert.That(uut.MaxPower, Is.EqualTo(value));

        }


    }
}
