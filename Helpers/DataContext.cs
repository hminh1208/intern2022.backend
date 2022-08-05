namespace WebApi.Helpers;

using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

public class DataContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<CategoryCity> CategoryCities { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Gendermanagement> Gendermanagemet { get; set; }

    public DbSet<Language> Languages { get; set; }  
    public DbSet<Category> Categories { get; set; }


    // public DbSet<Event> Events { get; set; }


    private readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sqlite database
        options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //seedingCity(modelBuilder);
    }

    //private void seedingCity(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<City>().HasData(
    //        new { Id = 1, Name = "Hà Nội", AbbName = "", CreatedAccountId = new Guid("687d9fd5-2752-4a96-93d5-0f33a49913c6"), CreatedDate = DateTime.Now },
    //        new { Id = 2, Name = "Hồ Chi Minh", AbbName = "", CreatedAccountId = new Guid("687d9fd5-2752-4a96-93d5-0f33a49913c6"), CreatedDate = DateTime.Now },
    //        new { Id = 3, Name = "Đà Nẵng", AbbName = "", CreatedAccountId = new Guid("687d9fd5-2752-4a96-93d5-0f33a49913c6"), CreatedDate = DateTime.Now },
    //        new { Id = 4, Name = "Quảng Nam", AbbName = "", CreatedAccountId = new Guid("687d9fd5-2752-4a96-93d5-0f33a49913c6"), CreatedDate = DateTime.Now },
    //        new { Id = 5, Name = "Lạng Sơn", AbbName = "", CreatedAccountId = new Guid("687d9fd5-2752-4a96-93d5-0f33a49913c6"), CreatedDate = DateTime.Now }
    //    );
    //}
}
