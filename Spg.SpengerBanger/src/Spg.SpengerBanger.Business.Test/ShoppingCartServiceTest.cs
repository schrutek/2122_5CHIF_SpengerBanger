using Microsoft.EntityFrameworkCore;
using Moq;
using Spg.SpengerBanger.Business.Test.Mock;
using Spg.SpengerBanger.Domain.Dtos;
using Spg.SpengerBanger.Domain.Exceptions;
using Spg.SpengerBanger.Domain.Interfaces;
using Spg.SpengerBanger.Domain.Model;
using Spg.SpengerBanger.Infrastructure;
using Spg.SpengerBanger.Services.ProductService;
using Spg.SpengerBanger.Services.ShoppingCartService;
using System;
using System.Linq;
using Xunit;

namespace Spg.SpengerBanger.Business.Test
{
    public class ShoppingCartServiceTest
    {
        public ShoppingCartServiceTest()
        { }

        #region -- Init ---------------------------------------------------------------------------

        private SpengerBangerContext GenerateMockDb()
        {
            DbContextOptions builder = new DbContextOptionsBuilder()
            .UseSqlite("Data Source=SpengerBangerTest.db").Options;

            SpengerBangerContext db = new SpengerBangerContext(builder);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.SeedTest();
            return db;
        }

        private (
            SpengerBangerContext context,
            IShoppingCartService shoppingCartService,
            ShoppingCart shoppingCart,
            Product product,
            User user)
            Prepare(
                string username,
                Guid shoppingCartId,
                int productId)
        {
            SpengerBangerContext context = GenerateMockDb();
            IShoppingCartService shoppingCartService = new ShoppingCartService(context, 
                new AuthServiceTestMock(username), 
                new ProductService(context));

            Product product = context.Products.SingleOrDefault(p => p.Id == productId);
            User user = context.Users.SingleOrDefault(p => p.EMail == username);
            ShoppingCart shoppingCart = context.ShoppingCarts.FirstOrDefault();
            if (shoppingCart is not null)
            {
                user = shoppingCart.UserNavigation;
            }
            return (context, shoppingCartService, shoppingCart, product, user);
        }

        #endregion -- Init ------------------------------------------------------------------------

        #region -- AddItem ------------------------------------------------------------------------

        [Fact]
        public void AddShoppingCartItemSuccess()
        {
            // Arrange
            SpengerBangerContext db = GenerateMockDb();

            ShoppingCartItemDto shoppingCartItemDto = new ShoppingCartItemDto()
            {
                Pieces = 1,
                PriceNett = 147.9939390150805M,
                ProductId = 1,
                ProductName = "Intelligent Wooden Chips",
                Tax = 20
            };

            IProductService productService = new ProductService(db);
            IAuthService authService = new AuthServiceTestMock("Levin_Hold@hotmail.com");
            IShoppingCartService shoppingCartService = new ShoppingCartService(db, authService, productService);

            Func<int> getShoppingCartItems = () =>
                db.ShoppingCarts.SingleOrDefault(s => s.Guid == new Guid("582C3776-5726-4349-A6FE-D640671878AB")).ShoppingCartItems.Count();
            int items = getShoppingCartItems();

            // Act
            shoppingCartService.AddItem(shoppingCartItemDto);

            // Assert
            Assert.Equal(items + 1, getShoppingCartItems());
        }

        [Fact]
        public void Test_AddItem_EmptyCart_Success()
        {
            // Arrange
            string username = "Levin_Hold@hotmail.com";
            Guid shoppingCartId = new Guid("582C3776-5726-4349-A6FE-D640671878AB");
            int productId = 1;

            var (context, shoppingCartService, shoppingCart, product, user) = Prepare(username, shoppingCartId, productId);
            ShoppingCartItemDto newShoppingCartItem = new() { ProductId = productId };

            // Act
            bool guid = shoppingCartService.AddItem(newShoppingCartItem);

            // Assert
            Assert.Single(context.ShoppingCarts.SingleOrDefault(s => s.Guid == shoppingCartId).ShoppingCartItems);
        }

        [Fact]
        public void Test_AddItem_IncreasePieces_Success()
        {
            // Arrange
            string username = "Levin_Hold@hotmail.com";
            Guid shoppingCartId = new Guid("582C3776-5726-4349-A6FE-D640671878AB");
            int productId = 1;

            var (context, shoppingCartService, shoppingCart, product, user) = Prepare(username, shoppingCartId, productId);
            Guid newGuid = Guid.NewGuid();
            shoppingCart.AddItem(new(product.Name, product.Tax, product.Nett, product, newGuid));
            context.SaveChanges();
            ShoppingCartItemDto newShoppingCartItem = new() { ProductId = productId };

            // Act
            bool guid = shoppingCartService.AddItem(newShoppingCartItem);

            // Assert
            Assert.Equal(2, context.ShoppingCarts.SingleOrDefault(s => s.Id == 1).ShoppingCartItems.First().Pieces);
        }

        [Fact]
        public void Test_AddItem_IncreasePieces_CheckStock_Success()
        {
            // Arrange
            string username = "Levin_Hold@hotmail.com";
            Guid shoppingCartId = new Guid("582C3776-5726-4349-A6FE-D640671878AB");
            int productId = 1;

            var (context, shoppingCartService, shoppingCart, product, user) = Prepare(username, shoppingCartId, productId);
            shoppingCart.AddItem(new(product.Name, product.Tax, product.Nett, product, Guid.NewGuid()));
            context.SaveChanges();
            ShoppingCartItemDto newShoppingCartItem = new() { ProductId = productId };

            // Act
            bool guid = shoppingCartService.AddItem(newShoppingCartItem);

            // Assert
            Assert.Equal(8, context.Products.SingleOrDefault(s => s.Id == productId).Stock);
        }

        [Fact]
        public void Test_AddItem_NoCart_Success()
        {
            // Arrange
            string username = "Levin_Hold@hotmail.com";
            int productId = 1;

            var (context, shoppingCartService, shoppingCart, product, user) = Prepare(username, new Guid(), productId);
            context.ShoppingCarts.RemoveRange(context.ShoppingCarts);
            context.SaveChanges();
            ShoppingCartItemDto newShoppingCartItem = new() { ProductId = productId };

            // Act/Assert
            _ = Assert.Throws<ServiceValidationException>(() => shoppingCartService.AddItem(newShoppingCartItem));
        }

        [Fact]
        public void Test_AddItem_OutOfStock_ValidationException()
        {
            // Arrange
            string username = "Levin_Hold@hotmail.com";
            Guid shoppingCartId = new Guid("582C3776-5726-4349-A6FE-D640671878AB");
            int productId = 1;

            var (context, shoppingCartService, shoppingCart, product, user) = Prepare(username, shoppingCartId, productId);
            shoppingCart.AddItem(new(product.Name, product.Tax, product.Nett, product, Guid.NewGuid()));
            context.SaveChanges();
            ShoppingCartItemDto newShoppingCartItem = new() { ProductId = productId, Pieces = 12 };

            // Act/Assert
            _ = Assert.Throws<ServiceValidationException>(() => shoppingCartService.AddItem(newShoppingCartItem));
        }

        #endregion -- AddItem ---------------------------------------------------------------------

        #region -- RemoveItem ---------------------------------------------------------------------

        [Fact]
        public void Test_RemoveItem_DecreasePieces_Success()
        {
            // Arrange
            string username = "Levin_Hold@hotmail.com";
            Guid shoppingCartId = new Guid("582C3776-5726-4349-A6FE-D640671878AB");
            int productId = 1;

            var (context, shoppingCartService, shoppingCart, product, user) = Prepare(username, shoppingCartId, productId);
            shoppingCart.AddItem(new(product.Name, product.Tax, product.Nett, product, Guid.NewGuid()) { Pieces = 5 });
            context.SaveChanges();
            Guid itemGuid = shoppingCart.ShoppingCartItems.First().Guid;
            ShoppingCartItemDto newShoppingCartItem = new() { ProductId = productId };

            // Act
            shoppingCartService.RemoveItem(productId, 1);

            // Assert
            Assert.Equal(4, context.ShoppingCarts.SingleOrDefault(s => s.Guid == shoppingCart.Guid).ShoppingCartItems.First().Pieces);
        }

        [Fact]
        public void Test_RemoveItem_DecreasePiecesToDeleteItem_Success()
        {
            // Arrange
            string username = "Levin_Hold@hotmail.com";
            Guid shoppingCartId = new Guid("582C3776-5726-4349-A6FE-D640671878AB");
            int productId = 1;

            var (context, shoppingCartService, shoppingCart, product, user) = Prepare(username, shoppingCartId, productId);
            shoppingCart.AddItem(new(product.Name, product.Tax, product.Nett, product, Guid.NewGuid()) { Pieces = 1 });
            context.SaveChanges();
            Guid itemGuid = shoppingCart.ShoppingCartItems.First().Guid;
            ShoppingCartItemDto newShoppingCartItem = new() { ProductId = productId };

            // Act
            shoppingCartService.RemoveItem(productId, 1);

            // Assert
            Assert.Empty(context.ShoppingCarts.SingleOrDefault(s => s.Guid == shoppingCart.Guid).ShoppingCartItems);
        }

        #endregion -- RemoveItem ------------------------------------------------------------------

    }
}