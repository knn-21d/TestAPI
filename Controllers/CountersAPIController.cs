using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;
using TestAPI.Services;

namespace TestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountersAPIController : ControllerBase
    {
        private readonly CountersService _service;

        public CountersAPIController(CountersService service)
        {
            _service = service;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Counter?>> GetCounter(int id)
        {
            try
            {
                return Ok(await _service.GetCounter(id));
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return StatusCode((int)ex.Response.StatusCode);
            }
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Counter>>> GetCounters()
        {
            return Ok(await _service.GetCounters());
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<int>> GetCountersCount()
        {
            return Ok(await _service.GetCountersCount());
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetCountersCountByKeyAndValuesMoreThanOne()
        {
            return Ok(await _service.GetCountersCountByKeyAndValuesMoreThanOne());
        }

        [HttpPost("{key:int}/{value:int}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Counter>> AddCounter(int key, int value)
        {
            return Ok(await _service.AddCounter(key, value));
        }

        [HttpPatch("{id:int}/{key:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<Counter>> ChangeKey(int id, int key)
        {
            try
            {
                return Ok(await _service.ChangeKey(id, key));
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return StatusCode((int)ex.Response.StatusCode);
            }
        }

        [HttpPatch("{id:int}/{value:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<Counter>> ChangeValue(int id, int value)
        {
            try
            {
                return Ok(await _service.ChangeKey(id, value));
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                return StatusCode((int)ex.Response.StatusCode);
            }
        }
    }
}
