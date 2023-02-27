using ClientOrders.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ClientOrders.Data.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            string? connectionString = config
                .GetConnectionString("ConnectionString");
            optionsBuilder.UseSqlServer(connectionString);
        }
        public void Seed()
        {
            if (Clients.Any() || Orders.Any())
                return;
            Clients.Add(new Client()
            {
                FirstName= "Amantur",
                SecondName= "Kaimov",
                PhoneNum= 0999102030,
                DateAdd= DateTime.Now,
            });
            Clients.Add(new Client()
            {
                FirstName = "Dimitrii",
                SecondName = "Vinogradnik",
                PhoneNum = 0505202020,
                DateAdd = DateTime.Now,
            });
            Clients.Add(new Client()
            {
                FirstName = "Ashirhan",
                SecondName = "Autahunov",
                PhoneNum = 0700140762,
                DateAdd = DateTime.Now,
            });
        }
    }
}
