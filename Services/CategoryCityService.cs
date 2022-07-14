namespace WebApi.Services
{
    public interface ICategoryCityService
    {
        List<CategoryCity> GetAll();
        CategoryCity GetById(int id);
        List<CategoryCity> GetById(string name, string shortname, int status);
        public void AddCategoryCity(string name, string shortname, int status);
        public void EditCategoryCity(int id, string name, string shortname, int status);
        public void DeleteCategoryCity(int id);

    }
    public class CategoryCityService : ICategoryCityService
    {
        private readonly DataContext dataContext;

        public CategoryCityService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async void AddCategoryCity(string name, string shortname, int status)
        {
            CategoryCity categoryCity = new CategoryCity();
            categoryCity.Name = name;
            categoryCity.ShortName = shortname;
            categoryCity.Status = status;
            this.dataContext.CategoryCities.Add(categoryCity);
            this.dataContext.SaveChanges();
        }

        public void DeleteCategoryCity(int id)
        {
            CategoryCity existedCategoryCity = this.GetById(id);
            if(existedCategoryCity != null)
            {
                this.dataContext.CategoryCities.Remove(existedCategoryCity);
                this.dataContext.SaveChanges();
            }    
        }

        public async void EditCategoryCity(int id, string name, string shortname, int status)
        {
            CategoryCity existedCategoryCity = this.GetById(id);
            if (existedCategoryCity != null)
            {
                existedCategoryCity.Name = name;
                existedCategoryCity.ShortName = shortname;
                existedCategoryCity.Status = status;
                this.dataContext.CategoryCities.Update(existedCategoryCity);
                this.dataContext.SaveChanges();
            }
        }

        public List<CategoryCity> GetAll()
        {
            return this.dataContext.CategoryCities.ToList();
        }

        public CategoryCity GetById(int id)
        {
            return this.dataContext.CategoryCities.Find(id);
        }

        public List<CategoryCity> GetById(string name, string shortname, int status)
        {
            return this.dataContext.CategoryCities.Where(record => record.Status == status && record.Name.Contains(name) 
            && record.ShortName.Contains(shortname)).ToList();
        }
    }
}
