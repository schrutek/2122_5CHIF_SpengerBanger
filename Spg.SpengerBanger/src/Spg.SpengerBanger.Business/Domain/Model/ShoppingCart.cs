using System.Collections.Generic;

namespace Spg.SpengerBanger.Business.Domain.Model
{
    public class ShoppingCart : EntityBase
    {
        public int UserId { get; set; }
        public User UserNavigation { get; set; }

        public IList<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
    }
}
