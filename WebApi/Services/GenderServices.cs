using WebApi.Models.Gendermanagements;
using WebApi.Enums;
using AutoMapper;
namespace WebApi.Services
{
    public interface IGenderServices
    {


        
        Task<List<GenderResponseDto>> Get(StatusEnum statusEnum, string keyWord, int page, int pageSize);

        Task<int> countAll(StatusEnum statusEnum, string keyWord);
        Task<GenderResponseDto> GetByID(int id);
        Task<GenderResponseDto> AddGendermanagement(GenderRequestDto genderRequestDto, Account account);
        Task<GenderResponseDto> UpdateGendermanagement(int id, GenderRequestDto genderRequestDto, Account account);
        Task<GenderResponseDto> DeleteGendermanagement(int id, Account account);
    }
    public class GenderServices : IGenderServices
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public GenderServices(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<GenderResponseDto> AddGendermanagement(GenderRequestDto genderRequestDto, Account account)
        {
            Gendermanagement newGendermanagement = new Gendermanagement(genderRequestDto.Name, account);
            if (_dataContext.Gendermanagemet.Any(x => x.Name == genderRequestDto.Name))
            {
                newGendermanagement = null;

            }
            else
            {
                this._dataContext.Add(newGendermanagement);
                await this._dataContext.SaveChangesAsync();
            }
            return _mapper.Map<Gendermanagement, GenderResponseDto>(newGendermanagement);
        }



        public async Task<GenderResponseDto> DeleteGendermanagement(int id, Account account)
        {
            Gendermanagement existGendermanagement = await _dataContext.Gendermanagemet.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (existGendermanagement != null)
            {
                existGendermanagement.Status = StatusEnum.DELETED;
                existGendermanagement.UpdatedDate = DateTime.Now;
                existGendermanagement.UpdatedAccount = account;
                await this._dataContext.SaveChangesAsync();
            }
            return _mapper.Map<Gendermanagement, GenderResponseDto>(existGendermanagement);

        }


        
        public async Task<List<GenderResponseDto>> Get(StatusEnum statusEnum, string keyWord, int page, int pageSize)

        
        {
            var listGendermanagements = await _dataContext.Gendermanagemet.Where(gendermanagement => gendermanagement.Status == statusEnum)
                .Skip(page * pageSize)
                .Take(pageSize)
                .OrderBy(gendermanagement => gendermanagement.Name)
                .ToListAsync();
            return _mapper.Map<List<Gendermanagement>, List<GenderResponseDto>>(listGendermanagements);
        }
       
        public async Task<int> countAll(StatusEnum statusEnum, string keyWord)
        {
            var totalGendermanagements = await _dataContext.Gendermanagemet.Where(gendermanagement => gendermanagement.Status == statusEnum)
                .Where(gendermanagement => gendermanagement.Name.Contains(keyWord ?? ""))
                .CountAsync();

            return totalGendermanagements;
        }

        public async Task<GenderResponseDto> GetByID(int id)
        {
            var existGenmanagement = await _dataContext.Gendermanagemet.Where(c => c.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<Gendermanagement, GenderResponseDto>(existGenmanagement);
        }

        public async Task<GenderResponseDto> UpdateGendermanagement(int id, GenderRequestDto genderRequestDto, Account account)
        {
            Gendermanagement existGendermanagement = await _dataContext.Gendermanagemet.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (existGendermanagement != null)
            {

                if (_dataContext.Gendermanagemet.Any(x => x.Name == genderRequestDto.Name))
                {
                    existGendermanagement = null;

                }
                else
                {

                    existGendermanagement.Name = genderRequestDto.Name;
                    existGendermanagement.UpdatedDate = DateTime.Now;
                    existGendermanagement.UpdatedAccount = account;
                    this._dataContext.Update(existGendermanagement);
                    await this._dataContext.SaveChangesAsync();
                }

            }
            return _mapper.Map<Gendermanagement, GenderResponseDto>(existGendermanagement);
        }
    }
} 
