using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Spg.SpengerBanger.Business.Test.Mock;

namespace Spg.SpengerBanger.Infrastructure
{
    public class TestContextFactory : IDesignTimeDbContextFactory<SpengerBangerTestContext>
    {
        private static string _connectionString;

        public SpengerBangerTestContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public SpengerBangerTestContext CreateDbContext(string[] args)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<SpengerBangerContext>();
            builder
                .UseSqlite(_connectionString)
                .EnableSensitiveDataLogging();

            return new SpengerBangerTestContext(builder.Options);
        }

        private static void LoadConnectionString()
        {
            _connectionString = $"Data Source=SpengerBangerTest.db";

            //var builder = new ConfigurationBuilder();
            //builder.AddJsonFile("appsettings.json", optional: false);

            //var configuration = builder.Build();

            //_connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}