namespace AirDucksProject.Models
{
    public class Measurement
    {
        public DateTime TimeStamp { get; set; }
        public float Reading { get; set; }

        public Sensor Sensor { get; set; }


        public Measurement()
        {

        }

        public Measurement(DateTime timeStamp, float reading)
        {
            TimeStamp = timeStamp;
            Reading = reading;

        }

        public void ValidateTime()
        { }
        public void ValidateReading()
        {

        }
        public void Validate()
        {

        }
    }
}
