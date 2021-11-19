using Spg.SpengerBanger.Business.Domain.Dtos;
using Spg.SpengerBanger.Business.Domain.Exceptions;
using Spg.SpengerBanger.Business.Domain.Interfaces;
using Spg.SpengerBanger.Business.Domain.Model;
using Spg.SpengerBanger.Business.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Services
{
    public class ShopService : IShopService
    {
        private SpengerBangerContext _dbContext;

        public ShopService(SpengerBangerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateShop(CreateShopDto newShop)
        {
            if (newShop is null) { 
                throw new ServiceException("Create Shop fehlgeschlagen!");
        }
            _dbContext.Add(newShop.ToShop());
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<Shop> ListAllShops()
        {
            return _dbContext.Shops;
        }
    }
}
