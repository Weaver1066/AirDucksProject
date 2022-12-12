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
        private SensorsManager sensorManager = new SensorsManager();
        private MeasurementsManager measurementsManager = new MeasurementsManager();
        // GET: api/<Controller>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> GetAllSensors()
        {
            return Ok(JsonConvert.SerializeObject(new { Sensors = sensorManager.GetAll(), measurements = measurementsManager.GetLatest() }));
        }


        //POST api/<Controller>
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

        // POST api/<Controller>
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

        // PUT api/<Controller>/5
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

        // DELETE api/<Controller>/5
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
