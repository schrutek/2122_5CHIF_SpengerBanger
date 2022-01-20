using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Domain.Model
{
    public class Category : EntityBase
    {
        protected Category() { }
        public Category(string name, Shop shopNavigation)
        {
            Name = name;
            ShopNavigation = shopNavigation;
        }

        public string Name { get; set; }

        public int ShopId { get; set; }
        public virtual Shop ShopNavigation { get; private set; } = null!;


        protected List<Product> _products = new();
        public virtual IReadOnlyList<Product> Products => _products;


        public Product AddProduct(Product newProduct)
        {
            if (newProduct is not null)
            {
                newProduct.CategoryId = Id;
                _products.Add(newProduct);
            }
            return newProduct;
        }
        public void AddProducts(List<Product> products)
        {
            _products.AddRange(products);
        }
    }
}
