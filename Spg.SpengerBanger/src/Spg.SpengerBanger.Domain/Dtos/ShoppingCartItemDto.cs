using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Domain.Dtos
{
    public class ShoppingCartItemDto
    {
        public int Pieces { get; set; } = 1;
        public int ProductId { get; set; }
    }
}
