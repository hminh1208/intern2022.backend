using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Gendermanagements;
using WebApi.Enums;
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
        [HttpGet("get-all")]
        public async Task<ActionResult<List<GenderResponseDto>>> getAsync(int page = 0, int pageSize = 10)
        {
            var gendemanagement = await _services.GetAll(StatusEnum.APPROVED, page, pageSize);
            return Ok(new
            {
                Results = gendemanagement
            }
            );
        }
        [HttpGet("search")]
        public async Task<ActionResult<List<object>>> searchAsync(string keyword, int page = 0, int pageSize = 10)
        {
            var gendemanagement = await _services.GetSearch(StatusEnum.APPROVED, keyword, page, pageSize);
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenderResponseDto>> deleteAsync(int id)
        {
            GenderResponseDto result = await _services.DeleteGendermanagement(id, Account);
            return Ok(result);
        }
    }
}
