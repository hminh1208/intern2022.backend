using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;

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
    }
}
