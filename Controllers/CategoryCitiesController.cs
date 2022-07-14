using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryCitiesController : BaseController
    {
        private readonly ICategoryCityService categoryCityService;

        public CategoryCitiesController(ICategoryCityService categoryCityService)
        {
            this.categoryCityService = categoryCityService;
        }

        [HttpGet("Get-all")]
        public List<CategoryCity> GetAll()
        {
            return categoryCityService.GetAll();
        }

        [HttpGet("Get-category-city-/{id}")]
        public CategoryCity GetById(int id)
        {
            return categoryCityService.GetById(id);
        }

        [HttpPost("Add-new-category-city")]
        public async void AddCategoryCity(string name, string shortname, int status)
        {
            if (name != null && shortname != null && status != 0)
            {
                categoryCityService.AddCategoryCity(name, shortname, status);
            }    
        }

        [HttpPost("Edit-category-cty-/{id}")]
        public async void EditCategoryCity(int id,string name, string shortname, int status)
        {
            if (name != null && shortname != null && status != 0)
            {
                CategoryCity categoryCity = categoryCityService.GetById(id);
                categoryCity.Name = name;
                categoryCity.ShortName = shortname;
                categoryCity.Status = status;
                categoryCityService.EditCategoryCity(id, name, shortname, status);
            }
        }

        [HttpDelete("Remove-category-city-/{id}")]
        public void DeleteCategoryCity(int id)
        {
            categoryCityService.DeleteCategoryCity(id);
        }
    }
}
