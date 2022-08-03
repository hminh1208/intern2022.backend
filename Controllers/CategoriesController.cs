using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Enums;
using WebApi.Models.Category;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetApprovedAsync(string keyword, int page = 0, int pageSize = 10)
        {
            var categories = await _categoryService.getAll(StatusEnum.APPROVED, keyword, page, pageSize);
            var total = await _categoryService.countAll(StatusEnum.APPROVED, keyword);
            return Ok(new
                {
                    Results = categories,
                    Total = total,
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponseDto>> GetAsync(int id)
        {
            var categories = await _categoryService.getById(id);
            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryResponseDto>> addAsync([FromBody]CategoryRequestDto cityDto)
        {
            CategoryResponseDto result = null;
            if (ModelState.IsValid)
            {
                result = await _categoryService.addAsync(cityDto, Account);
            }
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<CategoryResponseDto>> updateAsync(int id, [FromBody] CategoryRequestDto cityDto)
        {
            CategoryResponseDto result = null;
            if (ModelState.IsValid)
            {
                result = await _categoryService.updateAsync(id, cityDto, Account);
            }
            return Ok(result);
        }
    
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryResponseDto>> deleteAsync(int id)
        {
            CategoryResponseDto result = await _categoryService.deleteAsync(id, Account);
            return Ok(result);
        }
    }
}
