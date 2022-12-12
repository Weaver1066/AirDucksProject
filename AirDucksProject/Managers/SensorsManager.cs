using AirDucksProject.Database;
using AirDucksProject.MockData;
using AirDucksProject.Models;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using System.Diagnostics.Metrics;

namespace AirDucksProject.Managers
{
    public class SensorsManager
    {
        private static Dictionary<string, Sensor> Sensors = new Dictionary<string, Sensor>();
        private static int nextId = 0;
        public SensorsManager()
        {
            //TestCode Below. we will see how we use it
            if (Sensors.Count == 0) Sensors = GetSensorsAsync().Result.ToDictionary(s => s.Mac, s => s);
        }

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
        public async Task<Sensor> AddSensorAsync(string name, string mac)
        {
            Sensor newSensor = new Sensor() { Id = NextId().Result, Mac = mac, Name = name};
            newSensor.ValidateMac();
            newSensor.ValidateName();

            using (var context = new AirDucksDbContext())
            {
                context.Set<Sensor>().Add(newSensor);
                await context.SaveChangesAsync();
            }
            Sensors.Add(newSensor.Mac, newSensor);

            return newSensor;
        }

        public async Task<Sensor> UpdateSensorAsync(Sensor updatedSensor)
        {
            updatedSensor.ValidateMac();
            updatedSensor.ValidateName();

            using (var context = new AirDucksDbContext())
            {
                context.Set<Sensor>().Update(updatedSensor);
                await context.SaveChangesAsync();
            }
            Sensors.Remove(Sensors.Where(s => s.Value.Id == updatedSensor.Id).Select(s => s.Key).First());
            Sensors.Add(updatedSensor.Mac, updatedSensor);
            return updatedSensor;
        }

        public async Task<Sensor> DeleteSensorAsync(Sensor sensorToDelete)
        {
            using (var context = new AirDucksDbContext())
            {
                context.Set<Sensor>().Remove(sensorToDelete);
                await context.SaveChangesAsync();
            }
            Sensors.Remove(sensorToDelete.Mac);
            return sensorToDelete;
        }

        private async Task<int> NextId()
        {
            if(nextId == 0)
            {
                using (var context = new AirDucksDbContext())
                {
                    try
                    {
                        nextId = await context.Set<Sensor>().OrderByDescending(s => s.Id).Select(s => s.Id).FirstAsync();
                    }
                    catch
                    {
                        nextId = 0;
                    }
                }
            }
            return ++nextId;
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
