using AirDucksProject.Models;

namespace AirDucksProject.MockData
{
    public static class SensorData
    {
        static Sensor sensor1 = new Sensor("Falke", 1, "BF-1D-12-1F-C2-82");
        static Sensor sensor2 = new Sensor("Jonas", 2, "22-7A-54-85-49-6C");
        static Sensor sensor3 = new Sensor("Arian", 3, "C5-2E-D0-95-E8-12");
        static Sensor sensor4 = new Sensor("Patrick", 4, "A5-87-3A-9F-33-BF");
        private static Dictionary<string, Models.Sensor> mockSensors = new Dictionary<string, Models.Sensor>() 
        {
            {sensor1.Mac, sensor1},
            {sensor2.Mac, sensor2},
            {sensor3.Mac, sensor3},
            {sensor4.Mac, sensor4},
        };

        public static Dictionary<string, Sensor> GetMockSensors()
        {
            return mockSensors;
        }
    }
}
