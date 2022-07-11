using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Models.Cities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<ActionResult<List<City>>> GetAsync()
        {
            var cities = await _cityService.getAll();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetAsync(int id)
        {
            var cities = await _cityService.getById(id);
            return Ok(cities);
        }

        [HttpPost]
        public async Task<ActionResult<City>> addAsync([FromBody]CityDto cityDto)
        {
            City result = null;
            if (ModelState.IsValid)
            {
                result = await _cityService.addAsync(cityDto);
            }
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<City>> updateAsync(int id, [FromBody] CityDto cityDto)
        {
            City result = null;
            if (ModelState.IsValid)
            {
                result = await _cityService.updateAsync(id, cityDto);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<City>> deleteAsync(int id)
        {
            City result = await _cityService.deleteAsync(id);
            return Ok(result);
        }
    }
}
