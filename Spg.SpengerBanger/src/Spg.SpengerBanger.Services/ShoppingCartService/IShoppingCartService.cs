using Spg.SpengerBanger.Domain.Dtos;
using Spg.SpengerBanger.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Services.ShoppingCartService
{
    public interface IShoppingCartService
    {
        public ShoppingCartDto GetActive();

        public bool AddItem(ShoppingCartItemDto shoppingCartItemDto);

        public void EditItem(Guid guid, ShoppingCartDto item);

        public void RemoveItem(int productId, int pieces);
    }
}
