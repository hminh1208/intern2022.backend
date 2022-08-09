using AutoMapper;
using WebApi.Enums;
using WebApi.Models.CategoryCities;

namespace WebApi.Services
{
    public interface ICategoryCityService
    {
        Task<List<CategoryCityResponseDto>> GetAll(StatusEnum statusEnum, string keyword, int page, int pagesize);
        Task<CategoryCityResponseDto> GetById(int id);
        Task<CategoryCityResponseDto> AddCategoryCity(CategoryCityRequestDto categoryCityDto, Account account);
        Task<CategoryCityResponseDto> EditCategoryCity(int id, CategoryCityRequestDto categoryCityDto, Account account);
        Task<CategoryCityResponseDto> DeleteCategoryCity(int id, Account account);

        Task<List<CategoryCityResponseDto>> GetCategoryCityActive(string keyword, int page, int pagesize);

        Task<List<CategoryCityResponseDto>> GetCategoryCityDeleted(string keyword, int page, int pagesize);
        Task<int> countAll(StatusEnum statusEnum, string keyWord);
    }
    public class CategoryCityService : ICategoryCityService
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;

        public CategoryCityService(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<CategoryCityResponseDto> AddCategoryCity(CategoryCityRequestDto categoryCityDto, Account account)
        {
            CategoryCityResponseDto newCategoryCity = new CategoryCityResponseDto();
            if (dataContext.CategoryCities.Any(x => x.Name == categoryCityDto.Name) || dataContext.CategoryCities.Any(x => x.ShortName == categoryCityDto.ShortName))
            {
                return newCategoryCity = null;
            }
            else
            {
                CategoryCity newCity = new CategoryCity(categoryCityDto.Name, categoryCityDto.ShortName, account);
                this.dataContext.Add(newCity);
                await this.dataContext.SaveChangesAsync();
                return mapper.Map<CategoryCity, CategoryCityResponseDto>(newCity);
            }
            return newCategoryCity;
        }

        public async Task<int> countAll(StatusEnum statusEnum, string keyWord)
        {
            var totalCities = await dataContext.CategoryCities.Where(city => city.Status == (int)statusEnum)
                .Where(city => city.Name.Contains(keyWord ?? ""))
                .CountAsync();

            return totalCities;
        }

        public async Task<CategoryCityResponseDto> DeleteCategoryCity(int id, Account account)
        {
            CategoryCity existCity = await dataContext.CategoryCities.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (existCity != null)
            {
                existCity.Status = (int)StatusEnum.DELETED;
                existCity.UpdatedDate = DateTime.Now;
                existCity.UpdatedAccount = account;
                await this.dataContext.SaveChangesAsync();
            }
            return mapper.Map<CategoryCity, CategoryCityResponseDto>(existCity);
        }

        public async Task<CategoryCityResponseDto> EditCategoryCity(int id, CategoryCityRequestDto categoryCityDto, Account account)
        {
            CategoryCity existCity = await dataContext.CategoryCities.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (existCity != null)
            {
                existCity.Name = categoryCityDto.Name;
                existCity.ShortName = categoryCityDto.ShortName;
                existCity.UpdatedDate = DateTime.Now;
                existCity.UpdatedAccount = account;
                this.dataContext.Update(existCity);
                await this.dataContext.SaveChangesAsync();
            }
            return mapper.Map<CategoryCity, CategoryCityResponseDto>(existCity);
        }

        public async Task<List<CategoryCityResponseDto>> GetAll(StatusEnum statusEnum, string keyword, int page, int pagesize)
        {
            var listCategoryCities = await dataContext.CategoryCities.Where(categorycity => categorycity.Status == (int)statusEnum)
               .Where(categorycity => categorycity.Name.Contains(keyword ?? ""))
               .Skip(page * pagesize)
               .Take(pagesize)
               .OrderBy(categorycity => categorycity.Name)
               .ToListAsync();

            return mapper.Map<List<CategoryCity>, List<CategoryCityResponseDto>>(listCategoryCities);
        }

        public async Task<CategoryCityResponseDto> GetById(int id)
        {
            var existCity = await dataContext.CategoryCities.Where(c => c.Id == id).FirstOrDefaultAsync();
            return mapper.Map<CategoryCity, CategoryCityResponseDto>(existCity);
        }

        public async Task<List<CategoryCityResponseDto>> GetCategoryCityActive(string keyword, int page, int pagesize)
        {
            var listCategoryCities = await dataContext.CategoryCities.Where(categorycity => categorycity.Status == (int)StatusEnum.APPROVED)
                .Where(categorycity => categorycity.Name.Contains(keyword ?? ""))
                .Skip(page * pagesize)
                .Take(pagesize)
                .OrderBy(categorycity => categorycity.Name)
                .ToListAsync();

            return mapper.Map<List<CategoryCity>, List<CategoryCityResponseDto>>(listCategoryCities);
        }

        public async Task<List<CategoryCityResponseDto>> GetCategoryCityDeleted(string keyword, int page, int pagesize)
        {
            var listCategoryCities = await dataContext.CategoryCities.Where(categorycity => categorycity.Status == (int)StatusEnum.DELETED)
                .Where(categorycity => categorycity.Name.Contains(keyword ?? ""))
                .Skip(page * pagesize)
                .Take(pagesize)
                .OrderBy(categorycity => categorycity.Name)
                .ToListAsync();

            return mapper.Map<List<CategoryCity>, List<CategoryCityResponseDto>>(listCategoryCities);
        }

        public async Task<List<CategoryCity>> GetCategoryCityActive()
        {
            return await dataContext.CategoryCities.Where(c => c.Status == (int)StatusEnum.DRAFT).ToListAsync();
        }

        public async Task<List<CategoryCity>> GetCategoryCityDeleted()
        {
            return await dataContext.CategoryCities.Where(c => c.Status == (int)StatusEnum.DELETED).ToListAsync();
        }
    }
}
