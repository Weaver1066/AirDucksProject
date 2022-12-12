using AirDucksProject.Managers;
using AirDucksProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using AirDucksProject.Database;
using Microsoft.EntityFrameworkCore;

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
            expectedListLength = 1;

            Assert.AreEqual(expectedListLength, allSensors.Count);
        }

        [TestMethod]
        public void GetIdByMacTest_IdFoundSuccess()
        {
            SensorsManager testManager = new SensorsManager();
            int expectedId = 1;
            string validMAC = "4D-02-E3-87-36-9B";

            Assert.AreEqual(expectedId, testManager.GetIdByMac(validMAC));
        }

        [TestMethod]
        public void GetIdByMacTest_IdNotFoundFail()
        {
            SensorsManager testManager = new SensorsManager();
            int? expectedId = null;
            string invalidMAC = "4D-02-E3-87-36-8B";

            Assert.ThrowsException<NotFoundException>(() => testManager.GetIdByMac(invalidMAC));
        }

        [TestMethod]
        public void AddSensorTest_Success()
        {
            SensorsManager testManager = new SensorsManager();
            string newSensorMAC = "ED-28-E0-8D-9E-51";
            string newSensorName = "TestAddSensorName";

            Sensor addedSensor = testManager.AddSensorAsync(newSensorName, newSensorMAC).Result;

            Assert.IsTrue(testManager.GetAll().Contains(addedSensor));
        }
        [TestMethod]
        public void AddSensorTest_FailedValidation()
        {
            SensorsManager testManager = new SensorsManager();
            string validSensorMAC = "QD-28-E0-8D-9E-51";
            string validSensorName = "TestAddSensorName";
            string invalidSensorMAC = "3";
            string invalidSensorName = "";

            Assert.ThrowsException<AggregateException>(() => testManager.AddSensorAsync(validSensorName, invalidSensorMAC).Result);
            Assert.ThrowsException<AggregateException>(() => testManager.AddSensorAsync(invalidSensorName, validSensorMAC).Result);
        }

        [TestMethod]
        public void UpdateSensorTest_Success()
        {
            SensorsManager testManager = new SensorsManager();
            int existingId = 4;
            string newName = "TestUpdate";
            string newMac = "23-FF-0E-3F-0B-4C";

            Sensor updatedSensor = new Sensor(newName, existingId, newMac);

            Sensor result = testManager.UpdateSensorAsync(updatedSensor).Result;

            Assert.AreEqual(updatedSensor, result);
        }

        [TestMethod]
        public void DeleteSensorTest_Success()
        {
            SensorsManager sensorManager = new SensorsManager();
            MeasurementsManager measManager = new MeasurementsManager();

            Sensor sensorToDelete = sensorManager.AddSensorAsync("DeleteThisSensor", "1A-BC-82-8A-4B-38").Result;
            measManager.AddMeasurement(new Measurement(DateTime.Now.AddDays(2), 2f, sensorToDelete.Id));
            measManager.AddMeasurement(new Measurement(DateTime.Now.AddDays(3), 2f, sensorToDelete.Id));
            measManager.AddMeasurement(new Measurement(DateTime.Now, 2f, sensorToDelete.Id));

            //Sensor deletedSensor = sensorManager.DeleteSensorAsync(sensorToDelete).Result;

            //Assert.IsFalse(sensorManager.GetAll().Contains(deletedSensor));
        }
    }
}
