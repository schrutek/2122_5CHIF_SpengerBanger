using Spg.SpengerBanger.Business.Domain.Dtos;
using Spg.SpengerBanger.Business.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Domain.Interfaces
{
    public interface IShopService
    {
        IEnumerable<Shop> ListAllShops();

        public Task CreateShop(CreateShopDto newShop);
    }

}
