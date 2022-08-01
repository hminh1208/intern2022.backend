using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Gendermanagements;
using WebApi.Enums;
using WebApi.Authorization;
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendermanagementControllers : BaseController
    {
        private readonly IGenderServices _services;
        public GendermanagementControllers(IGenderServices services)
        {
            this._services = services;
        }
        
        [HttpGet("get")]
        public async Task<ActionResult<List<object>>> GetAsync(string keyword, int page = 0, int pageSize = 10)
        {
            var gendemanagement = await _services.Get(StatusEnum.APPROVED, keyword, page, pageSize);
            var total = await _services.countAll(StatusEnum.APPROVED, keyword);
            return Ok(new
            {
                Results = gendemanagement,
                Total = total
            }
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenderResponseDto>> getAsync(int id)
        {
            var gendermanagemment = await _services.GetByID(id);
            return Ok(gendermanagemment);
        }
        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult<GenderResponseDto>> addAsync([FromBody] GenderRequestDto genderRequestDto)
        {
            GenderResponseDto result = null;
            if (ModelState.IsValid)
            {
                result = await _services.AddGendermanagement(genderRequestDto, Account);
            }
            return Ok(result);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<GenderResponseDto>> updateAsync(int id, [FromBody] GenderRequestDto genderRequestDto)
        {
            GenderResponseDto result = null;
            if (ModelState.IsValid)
            {
                result = await _services.UpdateGendermanagement(id, genderRequestDto, Account);
            }
            return Ok(result);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenderResponseDto>> deleteAsync(int id)
        {
            GenderResponseDto result = await _services.DeleteGendermanagement(id, Account);
            return Ok(result);
        }
    }
}
