﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Domain.Model
{
    public class Price : EntityBase
    {
        public decimal Nett { get; set; }
        public int Tax { get; set; }

        public int CatPriceTypeId { get; set; }
        public CatPriceType CatPricetype { get; set; } = null!;

        public int ProductId { get; set; }
        public Product ProductNavigation { get; set; } = null!;
    }
}
