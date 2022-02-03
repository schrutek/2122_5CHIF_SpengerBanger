using Moq;
using Spg.SpengerBanger.Domain.Dtos;
using Spg.SpengerBanger.Domain.Interfaces;
using Spg.SpengerBanger.Domain.Model;
using Spg.SpengerBanger.Infrastructure;
using Spg.SpengerBanger.Services.ProductService;
using Spg.SpengerBanger.Services.ShoppingCartService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Spg.SpengerBanger.Business.Test
{
    public class ShoppingCartServiceTestMoq
    {
        private readonly ShoppingCartServiceWithRepo _shoppingCartService;

        private readonly Mock<IRepositoryBase<ShoppingCart>> _schoppingCartRepository = new Mock<IRepositoryBase<ShoppingCart>>();
        private readonly Mock<IAuthService> _authService = new Mock<IAuthService>();
        private readonly Mock<IProductService> _productService = new Mock<IProductService>();

        public ShoppingCartServiceTestMoq()
        {
            _shoppingCartService = new ShoppingCartServiceWithRepo(_schoppingCartRepository.Object, _authService.Object, _productService.Object);
        }

        [Fact]
        public void AddShoppingCartItemSuccess()
        {
            // Arrange
            Guid activeCartGuid = new Guid("582C3776-5726-4349-A6FE-D640671878AB");
            ShoppingCartItemDto shoppingCartItemDto = new ShoppingCartItemDto()
            {
                Pieces = 1,
                PriceNett = 147.9939390150805M,
                ProductId = 1,
                ProductName = "Intelligent Wooden Chips",
                Tax = 20
            };

            User user = new User(Gender.FEMALE, "Annelie", "Schlicht", "Levin_Hold@hotmail.com", new Guid("EC83E3C2-106C-4CB7-956D-E2B9F200D8B1"), new Address("Schlehdornstr.", "21839", "Lydiastadt"));
            Shop shop = new Shop("KG", "Huber - Savoia", "Kinderhausen 48c", "Switchable 3rd generation hierarchy", "expedite dot-com web-readiness", new Address("Am Steinberg", "35325", "Piaburg"), new Guid("8930863F-0B71-444D-9ACF-32B58AEC09EE"));
            Category category = new Category("Kategorie 100", shop); 
            CatPriceType catPriceType = new CatPriceType() { Key = "A", ShortName = "Aktion", Description = "Aktionspreis", ValidFrom = new DateTime(2021, 01, 01) };
            Product product = new Product("Intelligent Wooden Chips", "Handmade", "Cotton", "64508375", "5329128197047", 147.9939390150805M, 20, 10, catPriceType, category) { Id = 1 };
            ShoppingCart shoppingCart = new ShoppingCart(activeCartGuid, user) { Id = 1 };
            shoppingCart.AddItem(new ShoppingCartItem("", 20, 10, product, new Guid("4ccd1086-5d39-4a3a-883f-a9303e9da2e1")));

            // Setup Mock
            _authService.Setup(s => s.UserInfo())
                .Returns(new UserInfo(shoppingCart.Guid, shoppingCart.UserNavigation.EMail, UserRoles.Customer));
            _schoppingCartRepository.Setup(r => r.GetSingle(s => s.Guid == activeCartGuid, null, "ShoppingCartItems, UserNavigation"))
                .Returns(shoppingCart);
            _productService.Setup(s => s.GetProduct(shoppingCartItemDto.ProductId))
                .Returns(product);

            int pieces = shoppingCart.ShoppingCartItems[0].Pieces;

            // Act
            bool result = _shoppingCartService.AddItem(shoppingCartItemDto);

            // Assert
            Assert.Equal(pieces + 1, shoppingCart.ShoppingCartItems[0].Pieces);
        }
    }
}
