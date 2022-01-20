using System;

namespace Spg.SpengerBanger.Domain.Model
{
    public class ShoppingCartItem : EntityBase
    {
        protected ShoppingCartItem() { }
        public ShoppingCartItem(string name, int tax, decimal nett, Product product, Guid guid)
        {
            ProductId = product.Id;
            ProductNavigation = product;
            Name = name;
            Tax = tax;
            Nett = nett;
            Pieces = 1;
            LastChangeDate = DateTime.Now;
            Guid = guid;
        }

        public string Name { get; set; }
        public int Pieces { get; set; }
        public decimal Nett { get; set; }
        public int Tax { get; set; }
        public Guid Guid { get; set; }


        public int ProductId { get; set; }
        public virtual Product ProductNavigation { get; private set; }

    
        public int ShoppingCartId { get; set; }
        public virtual ShoppingCart ShoppingCartNavigation { get; private set; }
    }
}
