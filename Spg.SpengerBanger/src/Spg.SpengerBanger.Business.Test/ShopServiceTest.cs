using Microsoft.EntityFrameworkCore;
using Spg.SpengerBanger.Business.Infrastructure;
using Spg.SpengerBanger.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Spg.SpengerBanger.Business.Test
{
    public class ShopServiceTest
    {
        private readonly ShopService shopService;

        public ShopServiceTest()
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
            shopService = new ShopService(new SpengerBangerContext(options));
        }

        [Fact]
        public void TestGetShopList()
        {
            var erg = shopService.ListAllShops();
            Assert.True( erg is not null && erg.Any());

        }

    }
}
