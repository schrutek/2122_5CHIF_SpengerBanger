using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spg.SpengerBanger.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.ModelConfiguration.Configurations
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            // Weil es 2 Relationen vom ShoppingCart zu User gibt,
            // wird hier die 2. Relation explizit angegeben.
            builder.HasOne(c => c.UserNavigation).WithMany(e => e.ShoppingCarts);
        }
    }
}