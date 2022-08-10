using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Enums;
using WebApi.Models.Category;
using WebApi.Models.Cities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    
    public class CitiesController : BaseController
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetApprovedAsync(string keyword, int page = 0, int pageSize = 10)
        {
            var cities = await _cityService.getAll(StatusEnum.APPROVED, keyword, page, pageSize);
            var total = await _cityService.countAll(StatusEnum.APPROVED, keyword);
            return Ok(new
                {
                    Results = cities,
                    Total = total,
            }
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityResponseDto>> GetAsync(int id)
        {
            var cities = await _cityService.getById(id);
            return Ok(cities);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CityResponseDto>> addAsync([FromBody]CityRequestDto cityDto)
        {
            CityResponseDto result = null;
            if (ModelState.IsValid)
            {
                result = await _cityService.addAsync(cityDto, Account);
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost("{id}")]
        public async Task<ActionResult<CityResponseDto>> updateAsync(int id, [FromBody] CityRequestDto cityDto)
        {
            CityResponseDto result = null;
            if (ModelState.IsValid)
            {
                result = await _cityService.updateAsync(id, cityDto, Account);
            }
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<CityResponseDto>> deleteAsync(int id)
        {
            CityResponseDto result = await _cityService.deleteAsync(id, Account);
            return Ok(result);
        }
    }
}
