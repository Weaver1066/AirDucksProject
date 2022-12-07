using AirDucksProject.Managers;
using AirDucksProject.Models;
using Microsoft.AspNetCore.Mvc;
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
            return Ok(JsonConvert.SerializeObject(new {Sensors = sensorManager.GetAll(), measurements = measurementsManager.GetLatest()}));
        }

        // GET api/<Controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Controller>
        [HttpPost]
        public void Post([FromBody] (string, DateTime, float) input)
        {
            try 
            {
                measurementsManager.AddMeasurement(new Measurement(input.Item2, input.Item3, sensorManager.GetIdByMac(input.Item1)));
            }
            catch 
            {
                //do nothing
            }
            
        }

        // PUT api/<Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
