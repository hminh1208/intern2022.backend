using AutoMapper;
using WebApi.Enums;
using WebApi.Models.Category;
using WebApi.Models.Cities;

namespace WebApi.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDto>> getAll(StatusEnum statusEnum, string keyWord, int page, int pageSize);
        Task<int> countAll(StatusEnum statusEnum, string keyWord);
        Task<CategoryResponseDto> getById(int id);
        Task<CategoryResponseDto> addAsync(CategoryRequestDto CategoryDto, Account account);
        Task<CategoryResponseDto> updateAsync(int id, CategoryRequestDto CategoryDto, Account account);
        Task<CategoryResponseDto> deleteAsync(int id, Account account);
    }

    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryService(DataContext dataContext, IMapper mapper)
        {
            _context = dataContext;
            _mapper = mapper;
        }

        public async Task<CategoryResponseDto> addAsync(CategoryRequestDto CategoryDto, Account account)
        {
            Category newCategory = new Category(CategoryDto.Name, CategoryDto.ParentId, account);
            this._context.Add(newCategory);
            try
            {
                await this._context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
            return _mapper.Map<Category, CategoryResponseDto>(newCategory);
        }

        public async Task<CategoryResponseDto> deleteAsync(int id, Account account)
        {
            Category existCategory = await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (existCategory != null)
            {
                existCategory.Status = StatusEnum.DELETED;
                existCategory.UpdatedDate = DateTime.Now;
                existCategory.UpdatedAccount = account;
                await this._context.SaveChangesAsync();
            }
            return _mapper.Map<Category, CategoryResponseDto>(existCategory);
        }

        public async Task<List<CategoryResponseDto>> getAll(StatusEnum statusEnum, string keyWord, int page, int pageSize)
        {
            var listCities = await _context.Categories.Where(Category => Category.Status == statusEnum)
                .Where(Category => Category.Name.Contains(keyWord ?? ""))
                .Skip(page * pageSize)
                .Take(pageSize)
                .OrderBy(Category => Category.Name)
                .ToListAsync();

            return _mapper.Map<List<Category>, List<CategoryResponseDto>>(listCities);
        }

        public async Task<int> countAll(StatusEnum statusEnum, string keyWord)
        {
            var totalCities = await _context.Categories.Where(Category => Category.Status == statusEnum)
                .Where(Category => Category.Name.Contains(keyWord ?? ""))
                .CountAsync();

            return totalCities;
        }

        public async Task<CategoryResponseDto> getById(int id)
        {
            var existCategory = await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<Category, CategoryResponseDto>(existCategory);
        }

        public async Task<CategoryResponseDto> updateAsync(int id, CategoryRequestDto CategoryDto, Account account)
        {
            Category existCategory = await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (existCategory != null)
            {
                existCategory.Name = CategoryDto.Name;
                existCategory.UpdatedDate = DateTime.Now;
                existCategory.UpdatedAccount = account;
                this._context.Update(existCategory);
                await this._context.SaveChangesAsync();
            }
            return _mapper.Map<Category, CategoryResponseDto>(existCategory);
        }
    }
}
