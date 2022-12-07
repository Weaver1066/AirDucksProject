using AirDucksProject.Database;
using AirDucksProject.MockData;
using AirDucksProject.Models;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;

namespace AirDucksProject.Managers
{
    public class SensorsManager
    {
        public SensorsManager()
        {
            //if (Sensors.Count == 0) Sensors = SensorData.GetMockSensors

            //TestCode Below. we will see how we use it
            if (Sensors.Count == 0) Sensors = GetSensorsAsync().Result.ToDictionary(s => s.Mac, s => s);
        }
        private static Dictionary<string, Sensor> Sensors = new Dictionary<string, Sensor>();

        public List<Sensor> GetAll()
        {
            return new List<Sensor>(Sensors.Values);
        }
        public int GetIdByMac(string mac)
        {
            bool hasValue = Sensors.TryGetValue(mac, out Sensor sensor);
            if (hasValue) return sensor.Id;
            throw new NotFoundException("No sensor with that mac address found");
        }
        //Test code
        private async Task<IEnumerable<Sensor>> GetSensorsAsync()
        {
            using (var context = new AirDucksDbContext())
            {
                return await context.Set<Sensor>().AsNoTracking().ToListAsync();
            }
        }
    }
}
