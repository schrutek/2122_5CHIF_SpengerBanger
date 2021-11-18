using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Domain.Model
{
    public class CatPriceType : EntityBase
    {
        public string Key { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
