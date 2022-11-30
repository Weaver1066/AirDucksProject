using AirDucksProject.Database;
using AirDucksProject.MockData;
using AirDucksProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AirDucksProject.Managers
{
    public class SensorsManager
    {
        public SensorsManager()
        {
            //if (Sensors.Count == 0) Sensors = SensorData.GetMockSensors

            //TestCode Below. we will see how we use it
            if (Sensors.Count == 0) Sensors = GetObjectsAsync().Result.ToDictionary(s => s.Mac, s => s);
        }
        private static Dictionary<string, Sensor> Sensors = new Dictionary<string, Sensor>();

        public List<Sensor> GetAll()
        {
            return new List<Sensor>(Sensors.Values);
        }

        //Test code
        public async Task<IEnumerable<Sensor>> GetObjectsAsync()
        {
            using (var context = new AirDucksDbContext())
            {
                return await context.Set<Sensor>().AsNoTracking().ToListAsync();
            }
        }
    }
}
