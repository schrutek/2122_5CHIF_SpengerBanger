using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Domain.Model
{
    public class Shop : EntityBase
    {
        public string CompanySuffix { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string CatchPhrase { get; set; }
        public string Bs { get; set; }

        public IList<Category> Categories { get; set; } = new List<Category>();
    }
}
