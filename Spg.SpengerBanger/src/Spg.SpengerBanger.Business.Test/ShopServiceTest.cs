using Microsoft.EntityFrameworkCore;
using Spg.SpengerBanger.Business.Domain.Dtos;
using Spg.SpengerBanger.Business.Domain.Exceptions;
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

            var db = new SpengerBangerContext(options);
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Seed();

                Assert.True(true);

            shopService = new ShopService(new SpengerBangerContext(options));
        }

        [Fact]
        public void TestGetShopList()
        {
            var erg = shopService.ListAllShops();
            Assert.True( erg is not null && erg.Any());

        }

        [Fact]
        public void TestGetShopById()
        {
            var erg = shopService.GetShopById(1);
            Assert.True(erg is not null);

        }

        [Fact]
        public async void TestCreateShop()
        {
            var newShop = new CreateShopDto("Inc","Zeljko Group and Co","1050 Wien",
                "SpengerBenger beste ever", "competently strategize state of the art networks");
            await shopService.CreateShop(newShop);
            Assert.True(true);
        }

    }
}
