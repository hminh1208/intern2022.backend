using AutoMapper;
using WebApi.Enums;
using WebApi.Models.Cities;

namespace WebApi.Services
{
    public interface ICityService
    {
        Task<List<CityResponseDto>> getAll(StatusEnum statusEnum, string keyWord, int page, int pageSize);
        Task<int> countAll(StatusEnum statusEnum, string keyWord);
        Task<CityResponseDto> getById(int id);
        Task<CityResponseDto> addAsync(CityRequestDto cityDto, Account account);
        Task<CityResponseDto> updateAsync(int id, CityRequestDto cityDto, Account account);
        Task<CityResponseDto> deleteAsync(int id, Account account);
    }

    public class CityService : ICityService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CityService(DataContext dataContext, IMapper mapper)
        {
            _context = dataContext;
            _mapper = mapper;
        }

        public async Task<CityResponseDto> addAsync(CityRequestDto cityDto, Account account)
        {
            City newCity = new City(cityDto.Name, cityDto.AbbName, account);
            this._context.Add(newCity);
            await this._context.SaveChangesAsync();
            return _mapper.Map<City, CityResponseDto>(newCity);
        }

        public async Task<CityResponseDto> deleteAsync(int id, Account account)
        {
            City existCity = await _context.Cities.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (existCity != null)
            {
                existCity.Status = StatusEnum.DELETED;
                existCity.UpdatedDate = DateTime.Now;
                existCity.UpdatedAccount = account;
                await this._context.SaveChangesAsync();
            }
            return _mapper.Map<City, CityResponseDto>(existCity);
        }

        public async Task<List<CityResponseDto>> getAll(StatusEnum statusEnum, string keyWord, int page, int pageSize)
        {
            var listCities = await _context.Cities.Where(city => city.Status == statusEnum)
                .Where(city => city.Name.Contains(keyWord ?? ""))
                .Skip(page * pageSize)
                .Take(pageSize)
                .OrderBy(city => city.Name)
                .ToListAsync();

            return _mapper.Map<List<City>, List<CityResponseDto>>(listCities);
        }

        public async Task<int> countAll(StatusEnum statusEnum, string keyWord)
        {
            var totalCities = await _context.Cities.Where(city => city.Status == statusEnum)
                .Where(city => city.Name.Contains(keyWord ?? ""))
                .CountAsync();

            return totalCities;
        }

        public async Task<CityResponseDto> getById(int id)
        {
            var existCity = await _context.Cities.Where(c => c.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<City, CityResponseDto>(existCity);
        }

        public async Task<CityResponseDto> updateAsync(int id, CityRequestDto cityDto, Account account)
        {
            City existCity = await _context.Cities.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (existCity != null)
            {
                existCity.Name = cityDto.Name;
                existCity.AbbName = cityDto.AbbName;
                existCity.UpdatedDate = DateTime.Now;
                existCity.UpdatedAccount = account;
                this._context.Update(existCity);
                await this._context.SaveChangesAsync();
            }
            return _mapper.Map<City, CityResponseDto>(existCity);
        }
    }
}
