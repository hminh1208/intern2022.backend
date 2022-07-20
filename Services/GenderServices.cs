
namespace WebApi.Services
{
    public interface IGenderServices
    {
        List<Gendermanagement> GetAll();
        Gendermanagement GetByID(int id);
        public void  AddGendermanagement(string name,int status);
        public void UpdateGendermanagement(int id,string name,int status);
        public void DeleteGendermanagement(int id);
    }
    public class GenderServices : IGenderServices
    {
        private DataContext _dataContext;
        public GenderServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddGendermanagement(string name, int status)
        {
            Gendermanagement gendermanagemet = new Gendermanagement();
            gendermanagemet.Name = name;
            gendermanagemet.Status = status;
            this._dataContext.Gendermanagemet.Add(gendermanagemet);
            this._dataContext.SaveChanges();
        }

        public void DeleteGendermanagement(int id)
        {
            Gendermanagement gendermanagemet = this.GetByID(id);
            if (gendermanagemet != null)
            {
                if(gendermanagemet.Status == 0)
                {
                    gendermanagemet.Status = 1;
                }  
                else if(gendermanagemet.Status == 1)
                {
                    gendermanagemet.Status = 0;
                }    
                this._dataContext.Gendermanagemet.Update(gendermanagemet);
                this._dataContext.SaveChanges();
            }
        }

        public List<Gendermanagement> GetAll()
        {
            return this._dataContext.Gendermanagemet.ToList();
        }

        public Gendermanagement GetByID(int id)
        {
            return this._dataContext.Gendermanagemet.Where(g => g.Id == id).FirstOrDefault();
        }

        public void UpdateGendermanagement(int id, string name, int status)
        {
            Gendermanagement gendermanagemet = GetByID(id);
            if(gendermanagemet != null)
            {
                gendermanagemet.Name = name;
                gendermanagemet.Status=status;
                this._dataContext.Gendermanagemet.Update(gendermanagemet);
                this._dataContext.SaveChanges();
            }    
        }
    }
}
