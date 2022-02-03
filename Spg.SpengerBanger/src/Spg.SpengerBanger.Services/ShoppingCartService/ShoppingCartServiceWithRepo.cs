using Microsoft.EntityFrameworkCore;
using Spg.SpengerBanger.Domain.Dtos;
using Spg.SpengerBanger.Domain.Exceptions;
using Spg.SpengerBanger.Domain.Interfaces;
using Spg.SpengerBanger.Domain.Model;
using Spg.SpengerBanger.Infrastructure;
using Spg.SpengerBanger.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Services.ShoppingCartService
{
    public class ShoppingCartServiceWithRepo : IShoppingCartService
    {
        /// <summary>
        /// Hält die Instanz des DB-Context
        /// </summary>
        private readonly IRepositoryBase<ShoppingCart> _shoppingCartRepo;

        private readonly IAuthService _authService;
        private readonly IProductService _productService;

        /// <summary>
        /// Constructor-Injection
        /// </summary>
        /// <param name="dbContext"></param>
        public ShoppingCartServiceWithRepo(IRepositoryBase<ShoppingCart> shoppingCartRepo,
            IAuthService authService,
            IProductService productService)
        {
            _shoppingCartRepo = shoppingCartRepo;
            _authService = authService;
            _productService = productService;
        }

        public ShoppingCartDto GetActive()
        {
            Guid activecartGuid = _authService.UserInfo().ActiveShoppingCartId;
            ShoppingCart activeShoppingCart =_shoppingCartRepo
                .GetSingle(filter: s => s.Guid == activecartGuid, includeNavigationProperty: "ShoppingCartItems")
                    ?? throw new ServiceValidationException("Kein aktiver Warenkorb vorhanden!");

            return new ShoppingCartDto()
            {
                Guid = activeShoppingCart.Guid,
                ShoppingCartItems = activeShoppingCart.ShoppingCartItems
                    .AsQueryable().Select(s =>
                    new ShoppingCartItemDto()
                    {
                        Pieces = s.Pieces,
                        ProductId = s.ProductId,
                        PriceNett = s.Nett,
                        ProductName = s.Name,
                        Tax = s.Tax,
                    })
            };
        }

        // * Wenn das Produkt vorhanden ist, wird die Anzhal erhöht
        // * Nur der angemeldete User darf einen Artikel hinzufügen.
        //   (User Service über Dependency Injection übergeben)
        // * Product kann nur hinzugefügt werden, wenn noch auf Lager
        //   (Lagerstand in die DB adden. Achtung: SQLite ist ganz schwach bei Migrations!)
        // * Ist kein aktiver Warenkorb vorhanden (State), wird ein neuer Warenkorb angelegt
        public bool AddItem(ShoppingCartItemDto shoppingCartItemDto)
        {
            // Initialization:
            Guid activecartGuid = _authService.UserInfo().ActiveShoppingCartId;
            ShoppingCart activeShoppingCart = _shoppingCartRepo
                .GetSingle(filter: s => s.Guid == activecartGuid, includeNavigationProperty: "ShoppingCartItems, UserNavigation")
                    ?? throw new ServiceValidationException("Kein aktiver Warenkorb vorhanden!");
            Product selectedProduct = _productService.GetProduct(shoppingCartItemDto.ProductId);

            // Validation: (Kann man das ins Domain Model legen? (Was bei z.B. Überbuchungen))
            if ((selectedProduct.Stock - shoppingCartItemDto.Pieces) <= 0) // Besser: StockService
            {
                throw new ServiceValidationException("Der Artikel ist im Lager nicht mehr vorhanden.");
            }
            // ...

            // Act:
            Guid newGuid = Guid.NewGuid();
            ShoppingCartItem newShoppingCartItem = new ShoppingCartItem(selectedProduct.Name, selectedProduct.Tax, selectedProduct.Nett, selectedProduct, newGuid);
            newShoppingCartItem.Pieces = shoppingCartItemDto.Pieces;
            activeShoppingCart.AddItem(newShoppingCartItem);

            // Save
            try
            {
                _shoppingCartRepo.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ShoppingCartServiceException("Methode 'void AddItem(ShoppingCartItem)' ist fehlgeschlagen!", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ShoppingCartServiceException("Methode 'void AddItem(ShoppingCartItem)' ist fehlgeschlagen!", ex);
            }
            return true;
        }

        // * Anzahl einer Position ändern
        // * Nur der angemeldete User darf die Anzahl ändern
        // * Nur wenn genügend Artikel im Lager sind
        // * Wenn die Anzahl 0 ist, wird der Artikel aus dem Korb entfernt
        public void EditItem(Guid guid, ShoppingCartDto item)
        {
            throw new NotImplementedException();
        }

        // * Nur der angemeldete User darf einen Artikel entfernen.
        // * Wenn die Anzahl 0 ist, Artikel komplett aus WK löschen (OK)
        public void RemoveItem(int productId, int pieces)
        {
            // Initialization:
            Guid activecartGuid = _authService.UserInfo().ActiveShoppingCartId;
            ShoppingCart activeShoppingCart = _shoppingCartRepo
                .GetSingle(filter: s => s.Guid == activecartGuid, includeNavigationProperty: "ShoppingCartItems, UserNavigation")
                    ?? throw new ServiceValidationException("Kein aktiver Warenkorb vorhanden!");
            Product selectedProduct = _productService.GetProduct(productId);

            // Validation:
            // ...

            // Act:
            activeShoppingCart.RemoveItem(productId, pieces);

            // Save
            try
            {
                _shoppingCartRepo.SaveChanges();
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
