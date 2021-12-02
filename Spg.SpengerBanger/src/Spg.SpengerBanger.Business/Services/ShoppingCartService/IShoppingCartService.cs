using Spg.SpengerBanger.Business.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Services.ShoppingCartService
{
    public interface IShoppingCartService
    {
        IQueryable<ShoppingCartItem> ListByUser(Guid guid);

        public void AddItem(ShoppingCartItem newItem);

        public void EditItem(Guid guid, ShoppingCartItem item);

        public void RemoveItem(Guid guid);
    }
}
