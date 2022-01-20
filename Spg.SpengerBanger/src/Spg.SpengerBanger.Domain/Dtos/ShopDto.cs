using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Domain.Dtos
{
    public class ShopDto
    {
        public string CompanySuffix { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string CatchPhrase { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Bs { get; set; }
        public Guid Guid { get; set; }
        public CompanySuffixItemDto SelectedCompanySuffix { get; set; }
        public List<CompanySuffixItemDto> CompanySuffixItems { get; set; } = new();
    }
}
