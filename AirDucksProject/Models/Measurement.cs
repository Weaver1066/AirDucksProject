using System.ComponentModel.DataAnnotations.Schema;

namespace AirDucksProject.Models
{
    public class Measurement
    {
        public DateTime TimeStamp { get; set; }
        public float Reading { get; set; }
        public int SensorId { get; set; }
        public Sensor Sensor { get; set; }


        public Measurement()
        {

        }

        public Measurement(DateTime timeStamp, float reading, int sensorId)
        {
            TimeStamp = timeStamp;
            Reading = reading;
            SensorId = sensorId;
        }

        public void ValidateTime()
        {
            if (TimeStamp.Year < 2022) throw new ArgumentOutOfRangeException("Timestamp must be assigned");
        }
        public void ValidateReading()
        {
            if (Reading <= 0 || Reading >= 500) throw new ArgumentOutOfRangeException("Reading must not be below zero and must not be above 500");
        }
    }
}
