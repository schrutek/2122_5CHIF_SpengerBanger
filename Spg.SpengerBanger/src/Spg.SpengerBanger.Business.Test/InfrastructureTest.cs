using Microsoft.EntityFrameworkCore;
using Spg.SpengerBanger.Business.Infrastructure;
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
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=SpengerBanger.db")
                .Options;

            var db = new SpengerBangerContext(options);

            Assert.True(true);
         
        }
    }
}
