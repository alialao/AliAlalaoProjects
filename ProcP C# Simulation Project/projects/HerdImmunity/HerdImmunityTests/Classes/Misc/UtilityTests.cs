using Microsoft.VisualStudio.TestTools.UnitTesting;
using Immunization.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace Immunization.Classes.Tests
{
    [TestClass()]
    public class UtilityTests
    {
        [TestMethod()]
        public void GetHealthColorTest()
        {
            double healthPercentage = 50;
            byte r = 255;
            byte g = (byte)((healthPercentage - 50) * 5.1);
            byte b = g;
            Color color = new Color(r, g, b);
            Assert.AreEqual(Utility.GetHealthColor(healthPercentage), color);

            healthPercentage = 20;
            r = (byte)(128 + healthPercentage * 2.55);
            g = b = 0;
            color = new Color(r, g, b);
            Assert.AreEqual(Utility.GetHealthColor(healthPercentage), color);
        }


        [TestMethod()]
        public void GetDistanceTest()
        {
            //with longer real numbers after decimal point, distance accuracy varies per calculation
            double distance = Utility.GetDistance(2, 4, 18, 13);

            Assert.AreEqual(Math.Sqrt(Math.Pow((18 - 2), 2) + Math.Pow((13 - 4), 2)), distance);
        }
        

        [TestMethod()]
        public void GetSusceptibilityWeightedValueTest()
        {
            double min = 10;
            double max = 15;
            double susceptability = 20;
            double susceptabilityWeight = Utility.GetSusceptibilityWeightedValue(min, max, susceptability);

            var increment = (max - min) / 100;
            Assert.AreEqual(susceptabilityWeight, min + (increment * (100 - susceptability)));

        }
        
        
    }
}