using Microsoft.EntityFrameworkCore;
using Spg.SpengerBanger.Infrastructure;
using System;
using Xunit;

namespace Spg.SpengerBanger.Business.Test
{

    [Collection("Sequential")]
    public class InfrastructureTest
    {
        [Fact]
        public void GenerateDbFromContextTest()
        {
            SpengerBangerContext db = new TestContextFactory().CreateDbContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Seed();

            Assert.True(true);
        }
    }
}
