using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Domain.Model
{
    public class ShoppingCart : EntityBase
    {
        public int UserId { get; set; }
        public User UserNavigation { get; set; }

        public IList<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
    }
}
