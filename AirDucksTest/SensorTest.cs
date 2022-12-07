using AirDucksProject.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirDucksTest
{
    [TestClass]
    public class SensorTest
    {
        [TestMethod]
        public void SensorValidationName_ValidationSuccess()
        {
            Sensor testSensor = new Sensor();
            string name = "Test navn";
            testSensor.Name = name;

            testSensor.ValidateName();

            Assert.AreEqual(name, testSensor.Name);
        }

        [TestMethod]
        public void SensorValidationName_ValidationNullFail()
        {
            Sensor testSensor = new Sensor();

            Assert.ThrowsException<InvalidDataException>(() => testSensor.ValidateName());
        }

        [TestMethod]
        public void SensorValidationName_ValidationEmptyFail()
        {
            Sensor testSensor = new Sensor();
            string emptyName = "";
            testSensor.Name = emptyName;

            Assert.ThrowsException<InvalidDataException>(() => testSensor.ValidateName());
        }

        [TestMethod]
        public void SensorValidationMac_ValidationSuccess()
        {
            Sensor testSensor = new Sensor();
            string mac = "AB-04-DA-1A-37-B7";
            testSensor.Mac = mac;

            testSensor.ValidateMac();

            Assert.AreEqual(mac, testSensor.Mac);
        }

        [TestMethod]
        public void SensorValidationMac_ValidationNullFail()
        {
            Sensor testSensor = new Sensor();

            Assert.ThrowsException<InvalidDataException>(() => testSensor.ValidateMac());
        }

        [TestMethod]
        public void SensorValidationMac_ValidationEmptyFail()
        {
            Sensor testSensor = new Sensor();
            string emptyMac = "";
            testSensor.Mac = emptyMac;

            Assert.ThrowsException<InvalidDataException>(() => testSensor.ValidateMac());
        }
        [TestMethod]
        public void SensorValidationMac_ValidationInvalidFail()
        {
            Sensor testSensor = new Sensor();
            string emptyMac = "oijlasdkmasoddw";
            testSensor.Mac = emptyMac;

            Assert.ThrowsException<InvalidDataException>(() => testSensor.ValidateMac());
        }
    }
}