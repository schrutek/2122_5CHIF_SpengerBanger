using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Domain.Model
{
    public class ShoppingCartItem : EntityBase
    {
        public string Name { get; set; }

        public int Pax { get; set; }
        public decimal Nett { get; set; }
        public int Tax { get; set; }

        public int ProductId { get; set; }
        public Product ProductNavigation { get; set; }

        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCartNavigation { get; set; }
    }
}
