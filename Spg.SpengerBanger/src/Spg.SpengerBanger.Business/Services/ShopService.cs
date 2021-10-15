using Spg.SpengerBanger.Business.Domain.Model;
using Spg.SpengerBanger.Business.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Services
{
    public class ShopService
    {
        private SpengerBangerContext _dbContext;

        public ShopService(SpengerBangerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Shop> ListAllShops()
        {
            return _dbContext.Shops;
        }
    }
}
