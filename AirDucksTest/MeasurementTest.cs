using AirDucksProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirDucksTest
{
    [TestClass]
    public class MeasurementTest
    {
        [TestMethod]
        public void ValidateReading_Success()
        {
            Measurement testMeasurement = new Measurement();
            float expected = 20.25f;
            testMeasurement.Reading = expected;

            testMeasurement.ValidateReading();

            Assert.AreEqual(expected, testMeasurement.Reading);
        }

        [TestMethod]
        public void ValidateReading_LessThanZeroFail()
        {
            Measurement testMeasurement = new Measurement();
            float expected = -1f;
            testMeasurement.Reading = expected;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => testMeasurement.ValidateReading());
        }

        [TestMethod]
        public void ValidateTimeStamp_Success()
        {
            Measurement testMeasurement = new Measurement();
            DateTime expected = DateTime.Now;
            testMeasurement.TimeStamp = expected;

            testMeasurement.ValidateTime();

            Assert.AreEqual(expected, testMeasurement.TimeStamp);
        }

        [TestMethod]
        public void ValidateTimeStamp_NullFail()
        {
            Measurement testMeas = new Measurement();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => testMeas.ValidateTime());
        }
    }
}
