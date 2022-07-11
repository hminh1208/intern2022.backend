using WebApi.Models.Cities;

namespace WebApi.Services
{
    public interface ICityService
    {
        Task<List<City>> getAll();
        Task<City> getById(int id);
        Task<City> addAsync(CityDto cityDto);
        Task<City> updateAsync(int id, CityDto cityDto);
        Task<City> deleteAsync(int id);
    }
    public class CityService : ICityService
    {
        private readonly DataContext _context;

        public CityService(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<City> addAsync(CityDto cityDto)
        {
            City newCity = new City(cityDto.Name, cityDto.AbbName);
            this._context.Add(newCity);
            await this._context.SaveChangesAsync();
            return newCity;
        }

        public async Task<City> deleteAsync(int id)
        {
            City existCity = await this.getById(id);
            if (existCity != null)
            {
                this._context.Remove(existCity);
                await this._context.SaveChangesAsync();
            }
            return existCity;
        }

        public async Task<List<City>> getAll()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City> getById(int id)
        {
            return await _context.Cities.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<City> updateAsync(int id, CityDto cityDto)
        {
            City existCity = await this.getById(id);
            if(existCity != null)
            {
                existCity.Name = cityDto.Name;
                existCity.AbbName = cityDto.AbbName;
                this._context.Update(existCity);
                await this._context.SaveChangesAsync();
            }
            return existCity;
        }
    }
}
