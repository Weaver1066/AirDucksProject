﻿using AirDucksProject.Managers;
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
        public void addSensorTest_FailedValidation()
        {
            SensorsManager testManager = new SensorsManager();
            string validSensorMAC = "QD-28-E0-8D-9E-51";
            string validSensorName = "TestAddSensorName";
            string invalidSensorMAC = "3";
            string invalidSensorName = "";

            Assert.ThrowsException<AggregateException>(() => testManager.AddSensorAsync(validSensorName, invalidSensorMAC).Result);
            Assert.ThrowsException<AggregateException>(() => testManager.AddSensorAsync(invalidSensorName, validSensorMAC).Result);
        }
    }
}