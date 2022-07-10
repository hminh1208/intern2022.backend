using WebApi.Models.Cities;

namespace WebApi.Services
{
    public interface ICityService
    {
        List<City> getAll();
        City getById(int id);
        City add(CityDto cityDto);
        City update(int id, CityDto cityDto);
        City delete(int id);
    }
    public class CityService : ICityService
    {
        private readonly DataContext _context;

        public CityService(DataContext dataContext)
        {
            _context = dataContext;
        }

        public City add(CityDto cityDto)
        {
            City newCity = new City(cityDto.Name, cityDto.AbbName);
            this._context.Add(newCity);
            this._context.SaveChanges();
            return newCity;
        }

        public City delete(int id)
        {
            City existCity = this._context.Cities.Find(id);
            if (existCity != null)
            {
                this._context.Remove(existCity);
                this._context.SaveChanges();
            }
            return existCity;
        }

        public List<City> getAll()
        {
            return _context.Cities.ToList();
        }

        public City getById(int id)
        {
            return _context.Cities.Where(c => c.Id == id).FirstOrDefault();
        }

        public City update(int id, CityDto cityDto)
        {
            City existCity = this._context.Cities.Find(id);
            if(existCity != null)
            {
                existCity.Name = cityDto.Name;
                existCity.AbbName = cityDto.AbbName;
                this._context.Update(existCity);
                this._context.SaveChanges();
            }
            return existCity;
        }
    }
}
