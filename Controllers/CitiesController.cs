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
        public ActionResult<List<City>> Get()
        {
            var cities = _cityService.getAll();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public ActionResult<City> Get(int id)
        {
            var cities = _cityService.getById(id);
            return Ok(cities);
        }

        [HttpPost]
        public ActionResult<City> add([FromBody]CityDto cityDto)
        {
            City result = null;
            if (ModelState.IsValid)
            {
                result = _cityService.add(cityDto);
            }
            return Ok(result);
        }

        [HttpPost("{id}")]
        public ActionResult<City> update(int id, [FromBody] CityDto cityDto)
        {
            City result = null;
            if (ModelState.IsValid)
            {
                result = _cityService.update(id, cityDto);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<City> delete(int id)
        {
            City result = _cityService.delete(id);
            return Ok(result);
        }
    }
}
