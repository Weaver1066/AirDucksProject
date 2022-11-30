using AirDucksProject.Managers;
using AirDucksProject.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirDucksProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirDucksController : ControllerBase
    {
        private SensorsManager sensorManager = new SensorsManager();
        // GET: api/<Controller>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Sensor>> GetAllSensors()
        {
            return Ok(sensorManager.GetAll());
        }

        // GET api/<Controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
