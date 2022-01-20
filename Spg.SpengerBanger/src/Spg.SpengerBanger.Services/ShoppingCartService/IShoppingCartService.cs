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
        public IQueryable<ShoppingCartItemDetailsDto> GetActive();

        public Guid AddItem(ShoppingCartItemDto newItem);

        public void EditItem(Guid guid, ShoppingCartItemDto item);

        public void RemoveItem(int productId, int pieces);
    }
}
