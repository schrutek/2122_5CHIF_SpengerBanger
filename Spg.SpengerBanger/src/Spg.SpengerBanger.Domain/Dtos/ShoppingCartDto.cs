using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Domain.Dtos
{
    public class ShoppingCartDto
    {
        public Guid Guid { get; set; }

        public IEnumerable<ShoppingCartItemDto> ShoppingCartItems { get; set; }
    }
}
