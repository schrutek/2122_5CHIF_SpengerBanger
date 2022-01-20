using Spg.SpengerBanger.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Domain.Dtos
{
    public record CreateShopDto(string CompanySuffix, string Name, string Location,
       string CatchPhrase, string Bs, DateTime Closed)
    {

        public Shop ToShop()
        {
            return new Shop(CompanySuffix, Name, Location, CatchPhrase, Bs, new Address("", "", ""), Guid.NewGuid());
        }
    }
}
