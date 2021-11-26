using Spg.SpengerBanger.Business.Domain.Dtos;
using Spg.SpengerBanger.Business.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Services.ShopService
{
    public interface IShopService
    {
        IEnumerable<Shop> ListAllShops();

        Shop GetShopById(int id);

        Task CreateShop(CreateShopDto newShop);
    }

}
