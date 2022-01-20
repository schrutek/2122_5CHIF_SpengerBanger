using Spg.SpengerBanger.Domain.Model;
using System;
using System.Linq;

namespace Spg.SpengerBanger.Services.ProductService
{
    public interface IProductService
    {
        Product GetProduct(int id);
        IQueryable<Product> ListByCategory(Guid guid);

        public void IncreaseStock(int amount);

        public void DecreaseStock(int amount);

        public void AddItem(Product newItem);

        public void EditItem(Guid guid, Product item);

        public void RemoveItem(Guid guid);
    }
}
