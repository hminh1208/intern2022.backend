using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendermanagementControllers: BaseController
    {
        private readonly IGenderServices _services;
        public GendermanagementControllers(IGenderServices services)
        {
            this._services = services;
        }
        [HttpGet("get-all-gender")]
        public ActionResult<List<Gendermanagement>> GetAll() => _services.GetAll();
        [HttpGet("get-gender-ById/{id}")]
        public ActionResult<Gendermanagement> GetByID(int id) => this._services.GetByID(id);
        [HttpPost("add-gender")]
        public async void AddGenmanagemennt(string name,int status)
        {
            if(name != null && status != null)
            {
               
                _services.AddGendermanagement(name ,status);
            }
        }
        [HttpPut("edit-gender-ById/{id}")]
        public async void UpdateGendermanagement(int id,string name,int status)
        {
            if(name != null && status != null)
            {
                
                _services.UpdateGendermanagement(id,name,status);
            }
        }
        [HttpDelete("delete-gender")]
        public async void DeleteGendermanagement(int id)
        {
            _services.DeleteGendermanagement(id);
        }
    }
}
