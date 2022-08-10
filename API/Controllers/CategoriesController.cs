using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Enums;
using WebApi.Models.Category;
using WebApi.Models.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private ApiResponseDto responseDto = new ApiResponseDto();

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto>> GetApprovedAsync(string keyword, int page = 0, int pageSize = 10)
        {
            var categories = await _categoryService.GetAll(StatusEnum.APPROVED, keyword, page, pageSize);
            var total = await _categoryService.CountAll(StatusEnum.APPROVED, keyword);

            responseDto.Results = new PaginationResponseDto
            {
                Items = categories,
                TotalCount = total,
                CurrentPage = page,
                PageSize = pageSize
            };

            return responseDto;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseDto>> GetAsync(int id)
        {
            var categoryItem = await _categoryService.GetById(id);
            responseDto.Results = new SingleItemResponseDto
            {
                Item = categoryItem,
            };
            responseDto.StatusCode = categoryItem is not null ? 200 : 404;

            return responseDto;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto>> AddAsync(CategoryRequestDto cityDto)
        {
            CategoryResponseDto result = null;

            if (ModelState.IsValid)
            {
                result = await _categoryService.AddAsync(cityDto, Account);

                responseDto.Results = new SingleItemResponseDto
                {
                    Item = result,
                };
            }

            return responseDto;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<ApiResponseDto>> updateAsync(int id, [FromBody] CategoryRequestDto cityDto)
        {
            CategoryResponseDto result = null;
            if (ModelState.IsValid)
            {
                result = await _categoryService.updateAsync(id, cityDto, Account);

                responseDto.Results = new SingleItemResponseDto
                {
                    Item = result,
                };
            }
            else
            {
                responseDto.Results = ModelState.Values.SelectMany(v => v.Errors);
                responseDto.StatusCode = 400;
            }
            return responseDto;
        }
    
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseDto>> deleteAsync(int id)
        {
            CategoryResponseDto result = await _categoryService.deleteAsync(id, Account);

            responseDto.Results = new SingleItemResponseDto
            {
                Item = result,
            };

            return responseDto;
        }
    }
}
