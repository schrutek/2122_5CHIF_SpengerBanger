using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Domain.Dtos
{
    public class ShoppingCartItemDetailsDto : ShoppingCartItemDto
    {
        public string ProductName { get; set; }
        public decimal PriceNett { get; set; }
        public int Tax { get; set; }
    }
}
