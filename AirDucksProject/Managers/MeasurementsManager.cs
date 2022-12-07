using AirDucksProject.Database;
using AirDucksProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AirDucksProject.Managers
{
    public class MeasurementsManager
    {
        public Dictionary<int, Measurement> Latest = new Dictionary<int, Measurement>();

        public MeasurementsManager()
        {
            try
            {
                if (Latest.Count <= 0) Latest = DbGetLatestAsync().Result.ToDictionary(m => m.SensorId, m => m);
            }
            catch
            {
                Latest = new Dictionary<int, Measurement>();
            }
        }
        public async void AddMeasurement(Measurement measurement)
        {
            measurement.ValidateReading();
            measurement.ValidateTime();
            Latest[measurement.SensorId] = measurement;
            using (var context = new AirDucksDbContext())
            {
                context.Set<Measurement>().Add(measurement);
                await context.SaveChangesAsync();
            }
        }
        public List<Measurement> GetLatest()
        {
            return Latest.Values.ToList();
        }

        private async Task<IEnumerable<Measurement>> DbGetLatestAsync()
        {
            using (var context = new AirDucksDbContext())
            {
                List<Measurement> latestMeasurements = new List<Measurement>();
                foreach(int id in await context.Set<Sensor>().Select(i => i.Id).ToListAsync())
                {
                    latestMeasurements.Add(await context.Set<Measurement>().Where(m => m.SensorId == id).Select(m => m).OrderBy(m => m.TimeStamp).LastAsync());
                }
                return latestMeasurements;
            }
        }
    }
}
