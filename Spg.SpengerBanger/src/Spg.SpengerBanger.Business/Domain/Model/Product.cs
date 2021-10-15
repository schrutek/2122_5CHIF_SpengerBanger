using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Domain.Model
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public string ProductAdjective { get; set; }
        public string ProductMaterial { get; set; }
        public string Ean8 { get; set; }
        public string Ean13 { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
