using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Languages;
using WebApi.Enums;
using WebApi.Authorization;
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageControllers :BaseController
    {
        private readonly ILanguageService _languageService;
        public LanguageControllers(ILanguageService languageService)
        {
            _languageService = languageService;
        }
        [HttpGet("get")]
        public async Task<ActionResult<List<object>>> GetAsync(string keyword, int page = 0, int pageSize = 10)
        {
            var language = await _languageService.Get(StatusEnum.APPROVED, keyword, page, pageSize);
            var total = await _languageService.countAll(StatusEnum.APPROVED, keyword);
            return Ok(new
            {
                Results = language,
                Total = total
            }
            );
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageResponseDto>> GetById(int id)
        {
            var language = await _languageService.GetByID(id);
            return Ok(language);
        }
        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult<LanguageResponseDto>> AddAsync([FromBody]LanguageRequestDto languageRequestDto)
        {
            LanguageResponseDto result = null;
            if (ModelState.IsValid)
            {
                result = await _languageService.AddAsync(languageRequestDto, Account);
            }
            return Ok(result);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<LanguageResponseDto>> Update(int id, [FromBody]LanguageRequestDto languageRequestDto)
        {
            LanguageResponseDto result = null;
            if (ModelState.IsValid)
            {
                result = await _languageService.UpdateAsync(id, languageRequestDto, Account);
            }
            return Ok(result);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<LanguageResponseDto>> DeleteAsync(int id)
        {
            LanguageResponseDto result = await _languageService.DeleteAsync(id, Account);
            return Ok(result);
        }
        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<List<object>>> search(string keyword, string searchString)
        { 
            var language = await _languageService.SearchLanguage(StatusEnum.APPROVED, keyword, 0, 10, searchString);
            var total = await _languageService.countAll(StatusEnum.APPROVED, keyword);
            return Ok(new
            {
                Results = language,
                Total = total
            }
            );
        }
        [HttpGet("sort/{sortValue}")]
        public async Task<ActionResult<List<object>>> sort(string keyword, string sortValue)
        {
            var language = await _languageService.SortLanguage(StatusEnum.APPROVED, keyword, 0, 10, sortValue);
            var total = await _languageService.countAll(StatusEnum.APPROVED, keyword);
            return Ok(new
            {
                Results = language,
                Total = total
            }
            );
        }
    }
}
