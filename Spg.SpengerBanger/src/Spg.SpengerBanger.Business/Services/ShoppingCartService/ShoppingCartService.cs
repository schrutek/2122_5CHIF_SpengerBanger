using Spg.SpengerBanger.Business.Domain.Interfaces;
using Spg.SpengerBanger.Business.Domain.Model;
using Spg.SpengerBanger.Business.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Services.ShoppingCartService
{
    public class ShoppingCartService : IShoppingCartService
    {
        /// <summary>
        /// Hält die Instanz des DB-Context
        /// </summary>
        private readonly SpengerBangerContext _dbContext;
        private readonly IAuthService _authService;

        /// <summary>
        /// Constructor-Injection
        /// </summary>
        /// <param name="dbContext"></param>
        public ShoppingCartService(SpengerBangerContext dbContext, 
            IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }
        
        // * Guid als Parameter verwenden
        public IQueryable<ShoppingCartItem> ListByUser(Guid guid)
        {
            throw new NotImplementedException();
        }

        // * Product darf nur hinzugefügt werden, wenn er noch nicht vorhanden ist
        // * Nur der angemeldete User darf einen Artikel hinzufügen.
        //   (User Service über Dependency Injection übergeben)
        // * Product kann nur hinzugefügt werden, wenn noch auf Lager
        //   (Lagerstand in die DB adden. Achtung: SQLite ist ganz schwach bei Migrations!)
        // * 
        public void AddItem(ShoppingCartItem newItem)
        {
            throw new NotImplementedException();
        }

        // * Anzahl einer Position ändern
        // * Nur der angemeldete User darf die Anzahl ändern
        // * Nur wenn genügend Artikel im Lager sind
        // * Wenn die Anzahl 0 ist, wird der Artikel aus dem Korb entfernt
        public void EditItem(Guid guid, ShoppingCartItem item)
        {
            throw new NotImplementedException();
        }

        // * Nur der angemeldete User darf einen Artikel entfernen.
        // * 
        public void RemoveItem(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}
