namespace WebApi.Services
{
    public interface ICityService
    {
        List<City> getAll();
        City getById(int id);
    }
    public class CityService : ICityService
    {
        private readonly DataContext _context;

        public CityService(DataContext dataContext)
        {
            _context = dataContext;
        }

        public List<City> getAll()
        {
            return _context.Cities.ToList();
        }

        public City getById(int id)
        {
            return _context.Cities.Where(c => c.Id == id).FirstOrDefault();
        }
    }
}
