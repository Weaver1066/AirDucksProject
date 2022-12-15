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
            Measurement m1 = new Measurement() { Reading = 20f, SensorId = 3, TimeStamp = DateTime.Now };
            Measurement m2 = new Measurement() { Reading = 30f, SensorId = 3, TimeStamp = DateTime.Now };
            Measurement m3 = new Measurement() { Reading = 20f, SensorId = 3, TimeStamp = DateTime.Now };
            Measurement m4 = new Measurement() { Reading = 40f, SensorId = 3, TimeStamp = DateTime.Now };
            Measurement m5 = new Measurement() { Reading = 72f, SensorId = 4, TimeStamp = DateTime.Now };
            Measurement m6 = new Measurement() { Reading = 55f, SensorId = 4, TimeStamp = DateTime.Now };
            Measurement m7 = new Measurement() { Reading = 43f, SensorId = 4, TimeStamp = DateTime.Now };
            Measurement m8 = new Measurement() { Reading = 65f, SensorId = 5, TimeStamp = DateTime.Now }; 
            Measurement m9 = new Measurement() { Reading = 30f, SensorId = 5, TimeStamp = DateTime.Now };
            Measurement m10 = new Measurement() { Reading = 60f, SensorId = 6, TimeStamp = DateTime.Now };
            Measurement m11 = new Measurement() { Reading = 80f, SensorId = 6, TimeStamp = DateTime.Now };
            Measurement m12 = new Measurement() { Reading = 50f, SensorId = 6, TimeStamp = DateTime.Now };

            
            mmTest.AddMeasurement(m3);
            Thread.Sleep(500);
            mmTest.AddMeasurement(m4);
            Thread.Sleep(500);
            mmTest.AddMeasurement(m5);
            Thread.Sleep(500);
            mmTest.AddMeasurement(m6);
            Thread.Sleep(500);
            mmTest.AddMeasurement(m7);
            Thread.Sleep(500);
            mmTest.AddMeasurement(m8);
            Thread.Sleep(500);
            mmTest.AddMeasurement(m9);
            Thread.Sleep(500);
            mmTest.AddMeasurement(m10);
            Thread.Sleep(500);
            mmTest.AddMeasurement(m11);
            Thread.Sleep(500);
            mmTest.AddMeasurement(m12);


            //Assert.AreEqual(m1, mmTest.GetLatest().First());
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
        [TestMethod]
        public void GetAllReadingsBySensorId_IdFoundSuccess()
        {
            MeasurementsManager testManager = new MeasurementsManager();
            Measurement m1 = new Measurement() { Reading = 2f, SensorId = 6, TimeStamp = DateTime.Now };
            Measurement m2 = new Measurement() { Reading = 200f, SensorId = 6, TimeStamp = DateTime.Now };

            testManager.AddMeasurement(m1);
            testManager.AddMeasurement(m2);
            Assert.AreEqual(2, testManager.GetAllReadingsBySensorId(6).Result.Count());
        }
        public void GetAllReadingsBySensorId_IdFoundFail()
        {
            MeasurementsManager testManager = new MeasurementsManager();
            Assert.AreEqual(0, testManager.GetAllReadingsBySensorId(42).Result.Count());
        }
    }
}
