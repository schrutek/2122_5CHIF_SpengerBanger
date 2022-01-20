using Microsoft.EntityFrameworkCore;
using Spg.SpengerBanger.Domain.Dtos;
using Spg.SpengerBanger.Domain.Exceptions;
using Spg.SpengerBanger.Infrastructure;
using Spg.SpengerBanger.Services.ShopService;
using System;
using System.Linq;
using Xunit;

namespace Spg.SpengerBanger.Business.Test
{

    public class ShopServiceTest
    {
        public ShopServiceTest()
        { }

        private IShopService GenerateMockDb()
        {
            var options = new DbContextOptionsBuilder()
               .UseSqlite("Data Source=SpengerBanger.db")
               .Options;

            var db = new SpengerBangerContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Seed(); // Zufälliges zusammenstelen der DB-Daten (Bogus/Faker.js)

            Assert.True(true);

            return new ShopService(new SpengerBangerContext(options));
        }

        [Fact]
        public async void TestCreateShop()
        {
            IShopService shopService = GenerateMockDb();
            var newShop = new CreateShopDto("Inc", "Zeljko Group and Co", "1050 Wien",
                "SpengerBenger beste ever", "competently strategize state of the art networks", new DateTime(2021, 11, 28));
            await shopService.CreateShop(newShop);
            Assert.True(true);
        }

        [Fact]
        public async void TestGetShopList()
        {
            IShopService shopService = GenerateMockDb();
            var newShop = new CreateShopDto("Inc", "Zeljko Group and Co", "1050 Wien",
                "SpengerBenger beste ever", "competently strategize state of the art networks", new DateTime(2021, 11, 28));
            await shopService.CreateShop(newShop); // 1. Methode aus einem Service

            // Arrange (obwohl der obere Test zuerst läuft, wird hier eine neue DB erstellt und "jungfreulich" geseedet)
            //IShopService shopService = GenerateMockDb();

            // Act
            var erg = shopService.ListAllShops(); // 2. Methode aus einem Service

            // Assert
            Assert.True(erg is not null 
                && erg.Count() == 6);
        }

        [Fact]
        public void TestGetShopById()
        {
            // Arrange
            IShopService shopService = GenerateMockDb();

            // Act
            var erg = shopService.GetShopById(new Guid("8930863F-0B71-444D-9ACF-32B58AEC09EE"));

            // Assert
            Assert.True(erg is not null);
        }

        
        [Fact]
        public async void Test_ClosedOnSunday_Success()
        {
            IShopService shopService = GenerateMockDb();
            var newShop = new CreateShopDto("Inc", "Zeljko Group and Co", "1050 Wien",
                "SpengerBenger beste ever", "competently strategize state of the art networks", new DateTime(2021, 11, 28));

            await Assert.ThrowsAsync<ServiceException>(() => shopService.CreateShop(newShop));
        }
    }
}
