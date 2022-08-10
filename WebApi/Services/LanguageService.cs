using WebApi.Models.Languages;
using WebApi.Enums;
using AutoMapper;

namespace WebApi.Services
{
    public interface ILanguageService
    {
        Task<List<LanguageResponseDto>> Get(StatusEnum statusEnum, string keyWord, int page, int pageSize);
        Task<int> countAll(StatusEnum statusEnum, string keyWord);
        Task<LanguageResponseDto> GetByID(int id);
        Task<LanguageResponseDto> AddAsync(LanguageRequestDto languageRequestDto, Account account);
        Task<LanguageResponseDto> UpdateAsync(int id, LanguageRequestDto languageRequestDto, Account account);
        Task<LanguageResponseDto> DeleteAsync(int id, Account account);
        Task<List<LanguageResponseDto>> SearchLanguage(StatusEnum statusEnum, string keyWord, int page, int pageSize, string searchString);
        Task<List<LanguageResponseDto>> SortLanguage(StatusEnum statusEnum, string keyWord, int page, int pageSize, string sortValue);
    }
    public class LanguageService : ILanguageService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public LanguageService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<LanguageResponseDto> AddAsync(LanguageRequestDto languageRequestDto, Account account)
        {
            Language newLanguage = new Language(languageRequestDto.Name,languageRequestDto.ShortName, account);
            if (_dataContext.Languages.Any(x => x.Name == languageRequestDto.Name )|| _dataContext.Languages.Any(y => y.ShortName == languageRequestDto.ShortName))
            {
                newLanguage = null;

            }
            else
            {
                this._dataContext.Add(newLanguage);
                await _dataContext.SaveChangesAsync();
            }
            return _mapper.Map<Language, LanguageResponseDto>(newLanguage);
        }

        public async Task<int> countAll(StatusEnum statusEnum, string keyWord)
        {
            var totalLanguage = await _dataContext.Languages.Where(language =>language.Status == statusEnum)
                .Where(language =>language.Name.Contains(keyWord ?? "")).CountAsync();
            return totalLanguage;
        }

        public async Task<LanguageResponseDto> DeleteAsync(int id, Account account)
        {
            Language existLanguage = await _dataContext.Languages.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (existLanguage != null)
            {
                existLanguage.Status = StatusEnum.DELETED;
                existLanguage.UpdatedDate = DateTime.Now;
                existLanguage.UpdatedAccount = account;
                await _dataContext.SaveChangesAsync();
            }
            return _mapper.Map<Language, LanguageResponseDto>(existLanguage);
        }

        public async Task<List<LanguageResponseDto>> Get(StatusEnum statusEnum, string keyWord, int page, int pageSize)
        {
            var listLanguage = await _dataContext.Languages.Where(language =>language.Status == statusEnum)
                .Skip(page * pageSize)
                .Take(pageSize)
                .OrderBy(language => language.Name)
                .ToListAsync();
            return _mapper.Map<List<Language>, List<LanguageResponseDto>>(listLanguage);
        }

        public async Task<LanguageResponseDto> GetByID(int id)
        {
            var existLanguage = await _dataContext.Languages.Where(c => c.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<Language, LanguageResponseDto>(existLanguage);
        }
        public async Task<LanguageResponseDto> UpdateAsync(int id,LanguageRequestDto languageRequestDto,Account account)
        {
            Language existLanguage = await _dataContext.Languages.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (existLanguage != null)
            {
                if (_dataContext.Languages.Any(x => x.Name == languageRequestDto.Name) || _dataContext.Languages.Any(x => x.ShortName == languageRequestDto.ShortName))
                {
                    existLanguage = null;

                }
                else
                {

                    existLanguage.Name = languageRequestDto.Name;
                    existLanguage.ShortName = languageRequestDto.ShortName;
                    existLanguage.UpdatedDate = DateTime.Now;
                    existLanguage.UpdatedAccount = account;
                    this._dataContext.Update(existLanguage);
                    await this._dataContext.SaveChangesAsync();
                }
            }
            return _mapper.Map<Language, LanguageResponseDto>(existLanguage);
        }
        public async Task<List<LanguageResponseDto>> SearchLanguage(StatusEnum statusEnum, string keyWord, int page, int pageSize, string searchString)
        {
            var listLanguage = await _dataContext.Languages.Where(language => language.Status == statusEnum && 
                    (language.Name.Contains(searchString) || 
                        language.ShortName.Contains(searchString)))
                .Skip(page * pageSize)
                .Take(pageSize)
                .OrderBy(language => language.Name)
                .ToListAsync();
            return _mapper.Map<List<Language>, List<LanguageResponseDto>>(listLanguage);
        }
        public async Task<List<LanguageResponseDto>> SortLanguage(StatusEnum statusEnum, string keyWord, int page, int pageSize, string sortValue)
        {
            var listLanguage = from s in _dataContext.Languages select s;
            switch (sortValue)
            {
                case "name_desc":
                    listLanguage = listLanguage.OrderByDescending(s => s.Name);
                    break;
                case "shortName":
                    listLanguage = listLanguage.OrderBy(s => s.ShortName);
                    break;
                case "shortName_desc":
                    listLanguage = listLanguage.OrderByDescending(s => s.ShortName);
                    break;
                default:
                    listLanguage = listLanguage.OrderBy(s => s.Name);
                    break;
            }

            return _mapper.Map<List<Language>, List<LanguageResponseDto>>(await listLanguage.ToListAsync());
        }
    }

}
