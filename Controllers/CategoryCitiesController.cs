using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Enums;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CategoryCitiesController : BaseController
    {
        private readonly ICategoryCityService categoryCityService;

        public CategoryCitiesController(ICategoryCityService categoryCityService)
        {
            this.categoryCityService = categoryCityService;
        }
        [HttpGet]
        public async Task<ActionResult<object>> GetApprovedAsync(string keyword, int page = 0, int pageSize = 10)
        {
            var cities = await categoryCityService.GetAll(StatusEnum.APPROVED, keyword, page, pageSize);
            var total = await categoryCityService.countAll(StatusEnum.APPROVED, keyword);
            return Ok(new
            {
                Results = cities,
                Total = total,
            }
            );
        }

        [HttpGet("Get-all")]
        public async Task<ActionResult<List<CategoryCityResponseDto>>> GetAll(StatusEnum statusEnum, string keyword, int page = 0, int pagesize = 10)
        {
            var categorycities = await categoryCityService.GetAll(statusEnum, keyword, page, pagesize);
            return Ok(categorycities);
        }


        [HttpGet("Get-active")]
        public async Task<ActionResult<List<CategoryCityResponseDto>>> GetCategoryCityActive(string keyword, int page = 0, int pagesize = 10)
        {
            var newcategorycity = await categoryCityService.GetCategoryCityActive(keyword, page, pagesize);
            return Ok(newcategorycity);
        }

        [HttpGet("Get-Deleted")]
        public async Task<ActionResult<List<CategoryCityResponseDto>>> GetCategoryCityDeleted(string keyword, int page = 0, int pagesize = 10)
        {
            var newcategorycity = await categoryCityService.GetCategoryCityActive(keyword, page, pagesize);
            return Ok(newcategorycity);
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<CategoryCityResponseDto>> GetById(int id)
        {
            var newcategotycity = await categoryCityService.GetById(id);
            return Ok(newcategotycity);
        }

        [Authorize]
        [HttpPost("Add-new")]
        public async Task<ActionResult<CategoryCityResponseDto>> AddCategoryCity([FromBody] CategoryCityRequestDto categoryCityDto)
        {
            CategoryCityResponseDto result = null;
            if (ModelState.IsValid)
            {
                result = await categoryCityService.AddCategoryCity(categoryCityDto, Account);
            }    
            return Ok(result);
        }
        [Authorize]
        [HttpPost("Edit/{id}")]
        public async Task<ActionResult<CategoryCityResponseDto>> EditCategoryCity(int id,[FromBody] CategoryCityRequestDto categoryCityDto)
        {
            CategoryCityResponseDto result = null;
            if (ModelState.IsValid)
            {
                result = await categoryCityService.EditCategoryCity(id, categoryCityDto, Account);
            }
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("Remove/{id}")]
        public async Task<ActionResult<CategoryCityResponseDto>> DeleteCategoryCity(int id)
        {
            CategoryCityResponseDto result = await categoryCityService.DeleteCategoryCity(id, Account);
            return Ok(result);
        }
    }
}
