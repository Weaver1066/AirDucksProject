using AirDucksProject.Managers;
using AirDucksProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirDucksTest
{
    [TestClass]
    public class SensorsManagerTest
    {
        [TestMethod]
        public void GetAllTest()
        {
            SensorsManager testManager = new SensorsManager();
            List<Sensor> allSensors;
            int expectedListLength;

            allSensors = testManager.GetAll();
            expectedListLength = 4;

            Assert.AreEqual(expectedListLength, allSensors.Count);
        }
    }
}
