using Spg.SpengerBanger.Domain.Model;
using Spg.SpengerBanger.Infrastructure;
using System;
using System.Linq;

namespace Spg.SpengerBanger.Services.ProductService
{
    public class ProductService : IProductService
    {
        /// <summary>
        /// Hält die Instanz des DB-Context
        /// </summary>
        private readonly SpengerBangerContext _dbContext;

        /// <summary>
        /// Constructor-Injection
        /// </summary>
        /// <param name="dbContext"></param>
        public ProductService(SpengerBangerContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Product GetProduct(int id)
        {
            return _dbContext
                .Products
                .SingleOrDefault(p => p.Id == id);
        }

        public void IncreaseStock(int amount)
        {
            throw new NotImplementedException();
        }

        public void DecreaseStock(int amount)
        {
            throw new NotImplementedException();
        }

        public void AddItem(Product newItem)
        {
            throw new NotImplementedException();
        }

        public void EditItem(Guid guid, Product item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> ListByCategory(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}
