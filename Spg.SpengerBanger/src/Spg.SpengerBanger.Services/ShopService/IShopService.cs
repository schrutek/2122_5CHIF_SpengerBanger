using Spg.SpengerBanger.Domain.Dtos;
using Spg.SpengerBanger.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Services.ShopService
{
    public interface IShopService
    {
        IQueryable<ShopDto> ListAllShops();

        ShopDto GetShopById(Guid id);

        Task CreateShop(CreateShopDto newShop);

        Task UpdateShopAsync(ShopDto dto);
    }

}
