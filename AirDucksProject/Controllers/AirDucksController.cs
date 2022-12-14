using AirDucksProject.Managers;
using AirDucksProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirDucksProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirDucksController : ControllerBase
    {
        //Managers instance creation
        private SensorsManager sensorManager = new SensorsManager();
        private MeasurementsManager measurementsManager = new MeasurementsManager();

        //Returns a json string of an anon object consisting of two list properties
        //one containing all sensors and another containg the latest readings for each sensor
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> GetAllSensors()
        {
            return Ok(JsonConvert.SerializeObject(new { Sensors = sensorManager.GetAll(), measurements = measurementsManager.GetLatest() }));
        }

        //Takes a measurement as a Tuple, validates it and if it passes adds it to the database and local cache
        //if the measurement doesnt pass validation the method does nothing
        [Route("[action]")]
        [HttpPost]
        public void Post([FromBody] (string, DateTime, float) input)
        {
            try
            {
                Measurement measurementToAdd = new Measurement(input.Item2, input.Item3, sensorManager.GetIdByMac(input.Item1));
                measurementToAdd.ValidateReading();
                measurementToAdd.ValidateTime();
                measurementsManager.AddMeasurement(measurementToAdd);
            }
            catch
            {
                //do nothing
            }
        }

        //Takes a sensor object and adds it to the database and the local cache
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Sensor> PostSensor([FromBody] Sensor toAdd)
        {
            try
            {
                return Created("", sensorManager.AddSensorAsync(toAdd.Name, toAdd.Mac).Result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //Takes a sensor object and updates the local cache and database based on the mac and name properties
        //on the corresponding sensor
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<Sensor> Put([FromBody] Sensor sensor)
        {
            try
            {
                return Ok(sensorManager.UpdateSensorAsync(sensor).Result);
            }
            catch (Exception ex)
            {
                if(ex is ArgumentException) return BadRequest(ex.Message);
                if(ex is SqlException) return Conflict("This macAddress already exists");
                return NotFound("Something went wrong");
            }
        }

        // Takes a sensor and deletes it from the local cache and database
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Sensor> Delete([FromBody] Sensor sensor)
        {
            try
            {
                return Ok(sensorManager.DeleteSensorAsync(sensor).Result);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<List<Measurement>> GetAllReadingsBySensorId(int id)
        {
            List<Measurement> measurement = measurementsManager.GetAllReadingsBySensorId(id).Result.ToList();
            if (measurement.Count == 0) return NotFound("Id does not match sensor" + id);
            return Ok(measurement);
        }
    }
}
