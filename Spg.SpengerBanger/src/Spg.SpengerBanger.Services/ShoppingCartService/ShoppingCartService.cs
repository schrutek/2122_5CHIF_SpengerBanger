using Microsoft.EntityFrameworkCore;
using Spg.SpengerBanger.Domain.Dtos;
using Spg.SpengerBanger.Domain.Exceptions;
using Spg.SpengerBanger.Domain.Interfaces;
using Spg.SpengerBanger.Domain.Model;
using Spg.SpengerBanger.Infrastructure;
using Spg.SpengerBanger.Services.ProductService;

namespace Spg.SpengerBanger.Services.ShoppingCartService
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepositoryBase<ShoppingCart> _shoppingCartRepository;

        private readonly IAuthService _authService;
        private readonly IProductService _productService;

        /// <summary>
        /// Constructor-Injection
        /// </summary>
        /// <param name="dbContext"></param>
        public ShoppingCartService(IRepositoryBase<ShoppingCart> shoppingCartRepository, 
            IAuthService authService,
            IProductService productService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _authService = authService;
            _productService = productService;
        }
        
        // * Guid als Parameter verwenden
        public IQueryable<ShoppingCartItemDetailsDto> GetActive()
        {
            ShoppingCart activeShoppingCart = _shoppingCartRepository
                .GetSingle(filter: s => s.Guid == _authService.UserInfo().ActiveShoppingCartId, includeNavigationProperty: "ShoppingCartItems")
                    ?? throw new ServiceValidationException("Kein aktiver Warenkorb vorhanden!");

            return activeShoppingCart.ShoppingCartItems
                .AsQueryable().Select(s =>
                new ShoppingCartItemDetailsDto()
                {
                    Pieces = s.Pieces,
                    ProductId = s.ProductId,
                    PriceNett = s.Nett,
                    ProductName = s.Name,
                    Tax = s.Tax,
                });
        }

        // * Wenn das Produkt vorhanden ist, wird die Anzhal erhöht
        // * Nur der angemeldete User darf einen Artikel hinzufügen.
        //   (User Service über Dependency Injection übergeben)
        // * Product kann nur hinzugefügt werden, wenn noch auf Lager
        //   (Lagerstand in die DB adden. Achtung: SQLite ist ganz schwach bei Migrations!)
        // * Ist kein aktiver Warenkorb vorhanden (State), wird ein neuer Warenkorb angelegt
        public Guid AddItem(ShoppingCartItemDto shoppingCartItemDto)
        {
            // Initialization:
            ShoppingCart activeShoppingCart = _shoppingCartRepository
                .GetSingle(filter: s => s.Guid == _authService.UserInfo().ActiveShoppingCartId, includeNavigationProperty: "ShoppingCartItems")
                    ?? throw new ServiceValidationException("Kein aktiver Warenkorb vorhanden!");
            Product selectedProduct = _productService.GetProduct(shoppingCartItemDto.ProductId);

            // Validation: (Kann man das ins Domain Model legen? (Was bei z.B. Überbuchungen))
            if ((selectedProduct.Stock - shoppingCartItemDto.Pieces) <= 0) // StockService
            {
                throw new ServiceValidationException("Der Artikel ist im Lager nicht mehr vorhanden.");
            }
            // ...

            // Act:
            ShoppingCartItem newShoppingCartItem = new ShoppingCartItem(selectedProduct.Name, selectedProduct.Tax, selectedProduct.Nett, selectedProduct, Guid.NewGuid());
            newShoppingCartItem.Pieces = shoppingCartItemDto.Pieces;
            activeShoppingCart.AddItem(newShoppingCartItem);

            // Save
            try
            {
                _shoppingCartRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ShoppingCartServiceException("Methode 'void AddItem(Guid, ShoppingCartItem)' ist fehlgeschlagen!", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ShoppingCartServiceException("Methode 'void AddItem(Guid, ShoppingCartItem)' ist fehlgeschlagen!", ex);
            }
            return activeShoppingCart.Guid;
        }

        // * Anzahl einer Position ändern
        // * Nur der angemeldete User darf die Anzahl ändern
        // * Nur wenn genügend Artikel im Lager sind
        // * Wenn die Anzahl 0 ist, wird der Artikel aus dem Korb entfernt
        public void EditItem(Guid guid, ShoppingCartItemDto item)
        {
            throw new NotImplementedException();
        }

        // * Nur der angemeldete User darf einen Artikel entfernen.
        // * Wenn die Anzahl 0 ist, Artikel komplett aus WK löschen (OK)
        public void RemoveItem(int productId, int pieces)
        {
            // Initialization:
            ShoppingCart activeShoppingCart = _shoppingCartRepository
                .GetSingle(filter: s => s.Guid == _authService.UserInfo().ActiveShoppingCartId, includeNavigationProperty: "ShoppingCartItems")
                    ?? throw new ServiceValidationException("Kein aktiver Warenkorb vorhanden!");
            Product selectedProduct = _productService.GetProduct(productId);

            // Validation:
            // ...

            // Act:
            activeShoppingCart.RemoveItem(productId, pieces);

            // Save
            try
            {
                _shoppingCartRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ShoppingCartServiceException("Methode 'void AddItem(Guid, ShoppingCartItem)' ist fehlgeschlagen!", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ShoppingCartServiceException("Methode 'void AddItem(Guid, ShoppingCartItem)' ist fehlgeschlagen!", ex);
            }
        }
    }
}
