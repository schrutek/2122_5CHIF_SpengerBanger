using Microsoft.EntityFrameworkCore;
using Spg.SpengerBanger.Business.Infrastructure;
using System;
using Xunit;

namespace Spg.SpengerBanger.Business.Test
{
    public class InfrastructureTest
    {
        [Fact]
        public void GenerateDbFromContextTest()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=SpengerBanger.db")
                .Options;

            using (var db = new SpengerBangerContext(options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Seed();
                Assert.True(true);
            }
        }
    }
}
