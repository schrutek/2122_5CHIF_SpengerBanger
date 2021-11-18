using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Domain.Model
{
    public class Category : EntityBase
    {
        public string Name { get; set; }

        public int ShopId { get; set; }
        public Shop ShopNavigation { get; set; } = null!;

        public IList<Product> Products { get; set; } = new List<Product>();
    }
}
