using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Spg.SpengerBanger.Infrastructure
{
    public class ContextFactory : IDesignTimeDbContextFactory<SpengerBangerContext>
    {
        private static string _connectionString;

        public SpengerBangerContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public SpengerBangerContext CreateDbContext(string[] args)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<SpengerBangerContext>();
            builder.UseSqlite(_connectionString);

            return new SpengerBangerContext(builder.Options);
        }

        private static void LoadConnectionString()
        {
            _connectionString = $"Data Source=SpengerBanger.db";

            //var builder = new ConfigurationBuilder();
            //builder.AddJsonFile("appsettings.json", optional: false);

            //var configuration = builder.Build();

            //_connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
