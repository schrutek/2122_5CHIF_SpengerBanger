
using Spg.SpengerBanger.Business.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Domain.Dtos
{
    public record CreateShopDto(string CompanySuffix, string Name, string Location,
       string CatchPhrase, string Bs)
    {

        public Shop ToShop()
        {
            return new Shop() {
                CompanySuffix = CompanySuffix,
                Name = Name,
                Location = Location,
                CatchPhrase = CatchPhrase,
                Bs = Bs
            };
        }
    }
}
