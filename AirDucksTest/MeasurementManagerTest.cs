using AirDucksProject.Managers;
using AirDucksProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirDucksTest
{
    [TestClass]
    public class MeasurementManagerTest
    {
        [TestMethod]
        public void AddMeasurementTest_Success()
        {
            MeasurementsManager mmTest = new MeasurementsManager();
            Measurement m1 = new Measurement() { Reading = 2f, SensorId = 1, TimeStamp = DateTime.Now };

            mmTest.AddMeasurement(m1);

            Assert.AreEqual(m1, mmTest.GetLatest().First());
        }

        [TestMethod]
        public void GetByLatest_Success()
        {
            MeasurementsManager mmTest = new MeasurementsManager();
            Measurement m1 = new Measurement() { Reading = 2f, SensorId = 1, TimeStamp = DateTime.Now };
            Measurement m2 = new Measurement() { Reading = 200f, SensorId = 1, TimeStamp = DateTime.Now };

            mmTest.AddMeasurement(m1);
            mmTest.AddMeasurement(m2);

            Assert.AreEqual(m2, mmTest.GetLatest().First());
        }
    }
}
