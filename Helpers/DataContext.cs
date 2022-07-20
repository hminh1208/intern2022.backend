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
        seedingCity(modelBuilder);
    }

    private void seedingCity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>().HasData(
            new { Id = 1, Name = "Hà Nội", AbbName = "" },
            new { Id = 2, Name = "Hồ Chi Minh", AbbName = "" },
            new { Id = 3, Name = "Đà Nẵng", AbbName = "" },
            new { Id = 4, Name = "Quảng Nam", AbbName = "" },
            new { Id = 5, Name = "Lạng Sơn", AbbName = "" }
        );
    }
}