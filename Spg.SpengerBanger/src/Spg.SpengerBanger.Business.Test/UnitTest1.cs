using Microsoft.EntityFrameworkCore;
using Spg.SpengerBanger.Business.Infrastructure;
using System;
using Xunit;

namespace Spg.SpengerBanger.Business.Test
{
    public class UnitTest1
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
                Assert.True(true);
            }
        }
    }
}
