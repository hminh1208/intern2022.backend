using WebApi.Enums;
using WebApi.Models.CategoryCities;

namespace WebApi.Services
{
    public interface ICategoryCityService
    {
        Task<List<CategoryCity>> GetAll();
        Task<CategoryCity> GetById(int id);
        Task<CategoryCity> AddCategoryCity(CategoryCityDto categoryCityDto);
        Task<CategoryCity> EditCategoryCity(int id, CategoryCityDto categoryCityDto);
        Task<CategoryCity> DeleteCategoryCity(int id);

        Task<List<CategoryCity>> GetCategoryCityActive();

        Task<List<CategoryCity>> GetCategoryCityDeleted();
    }
    public class CategoryCityService : ICategoryCityService
    {
        private readonly DataContext dataContext;

        public CategoryCityService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<CategoryCity> AddCategoryCity(CategoryCityDto categoryCityDto)
        {
            CategoryCity newCategoryCity = new CategoryCity();
            if (dataContext.CategoryCities.Any(x => x.Name == categoryCityDto.Name) || dataContext.CategoryCities.Any(x => x.ShortName == categoryCityDto.ShortName))
            {
                return newCategoryCity = null;
            }  
            else
            {
                newCategoryCity.Name = categoryCityDto.Name;
                newCategoryCity.ShortName = categoryCityDto.ShortName;
                newCategoryCity.Status = (int)StatusEnum.DRAFT;

                this.dataContext.Add(newCategoryCity);
                await this.dataContext.SaveChangesAsync();
                return newCategoryCity;
            }    
            return newCategoryCity;
        }

        public async Task<CategoryCity> DeleteCategoryCity(int id)
        {
            CategoryCity newCategoryCity = await this.GetById(id);
            if (newCategoryCity != null)
            {
                newCategoryCity.Status = (int)StatusEnum.DELETED;


                this.dataContext.Entry(newCategoryCity).State = EntityState.Modified;
                await this.dataContext.SaveChangesAsync();
            }
            return newCategoryCity;
        }

        public async Task<CategoryCity> EditCategoryCity(int id, CategoryCityDto categoryCityDto)
        {
            CategoryCity newCategoryCity = await this.GetById(id);
            if (newCategoryCity != null && newCategoryCity.Status == 0)
            {
                newCategoryCity.Name = categoryCityDto.Name;
                newCategoryCity.ShortName = categoryCityDto.ShortName;;
                
                this.dataContext.Update(newCategoryCity);
                await this.dataContext.SaveChangesAsync();
            }
            return newCategoryCity;
        }

        public async Task<List<CategoryCity>> GetAll()
        {
            return await dataContext.CategoryCities.ToListAsync();
        }

        public async Task<CategoryCity> GetById(int id)
        {
            return dataContext.CategoryCities.FirstOrDefault(c => c.Id == id);
        }

        public async Task<List<CategoryCity>> GetCategoryCityActive()
        {
            return dataContext.CategoryCities.Where(c => c.Status == (int)StatusEnum.DRAFT).ToList();
        }

        public async Task<List<CategoryCity>> GetCategoryCityDeleted()
        {
            return dataContext.CategoryCities.Where(c => c.Status == (int)StatusEnum.DELETED).ToList();
        }
    }
}
