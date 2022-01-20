using Spg.SpengerBanger.Domain.Model;
using Spg.SpengerBanger.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Services.CategoryService
{
    public  class CategoryService : ICategoryService
    {
        private readonly SpengerBangerContext _dbContext;

        public CategoryService(SpengerBangerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Category> GetAllByShop(Guid shopId)
        {
            return _dbContext.Categories.Where(c => c.ShopNavigation.Guid == shopId);
        }
    }
}
