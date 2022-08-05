using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Models.CategoryCities;

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

        [HttpGet("Get-all")]
        public async Task<ActionResult<List<CategoryCity>>> GetAll()
        {
            var newcategorycity = await categoryCityService.GetAll();
            return Ok(newcategorycity);
        }


        [HttpGet("Get-active")]
        public async Task<ActionResult<List<CategoryCity>>> GetCategoryCityActive()
        {
            var newcategorycity = await categoryCityService.GetCategoryCityActive();
            return Ok(newcategorycity);
        }

        [HttpGet("Get-Deleted")]
        public async Task<ActionResult<List<CategoryCity>>> GetCategoryCityDeleted()
        {
            return Ok(await categoryCityService.GetCategoryCityDeleted());
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<CategoryCity>> GetById(int id)
        {
            var newcategotycity = await categoryCityService.GetById(id);
            return Ok(newcategotycity);
        }

        [HttpPost("Add-new")]
        public async Task<ActionResult<CategoryCity>> AddCategoryCity([FromBody] CategoryCityDto categoryCityDto)
        {
            CategoryCity result = null;
            if (ModelState.IsValid)
            {
                result = await categoryCityService.AddCategoryCity(categoryCityDto);
            }    
            return Ok(result);
        }


        [HttpPost("Edit/{id}")]
        public async Task<ActionResult<CategoryCity>> EditCategoryCity(int id,[FromBody] CategoryCityDto categoryCityDto)
        {
            CategoryCity result = null;
            if (ModelState.IsValid)
            {
                result = await categoryCityService.EditCategoryCity(id, categoryCityDto);
            }
            return Ok(result);
        }

        [HttpDelete("Remove/{id}")]
        public async Task<ActionResult<CategoryCity>> DeleteCategoryCity(int id)
        {
            CategoryCity result = await categoryCityService.DeleteCategoryCity(id);
            return Ok(result);
        }
    }
}
