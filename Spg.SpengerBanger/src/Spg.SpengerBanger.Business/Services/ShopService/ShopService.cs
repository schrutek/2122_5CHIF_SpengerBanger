using Spg.SpengerBanger.Business.Domain.Dtos;
using Spg.SpengerBanger.Business.Domain.Exceptions;
using Spg.SpengerBanger.Business.Domain.Model;
using Spg.SpengerBanger.Business.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Services.ShopService
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
            // Bedungungen:
            // * Der Shop soll immer und nur Sonntags geschlossen sein
            // * 

            if (newShop.Closed.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new ServiceException("Sonntag ist Ruhetag!");
            }

            if (newShop is null)
            {
                throw new ServiceException("Create Shop fehlgeschlagen!");
            }
            _dbContext.Add(newShop.ToShop());
            await _dbContext.SaveChangesAsync();
        }

        public Shop GetShopById(int id)
        {
            var data = _dbContext.Shops.ToList();

            return data.Where(s => s.Id == id).First();
        }

        public IEnumerable<Shop> ListAllShops()
        {
            return _dbContext.Shops;
        }
    }
}
