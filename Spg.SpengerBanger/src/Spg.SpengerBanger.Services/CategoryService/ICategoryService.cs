using Spg.SpengerBanger.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Services.CategoryService
{
    public interface ICategoryService
    {
        public IQueryable<Category> GetAllByShop(Guid shopId);
    }
}
