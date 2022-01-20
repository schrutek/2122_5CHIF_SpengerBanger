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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Für die Address Value Objects in User und Shop
            builder.OwnsOne(c => c.Address);
            // Index setzen, weil danach oft gesucht werden wird.
            // Außerdem ist die EMail-Adresse eindeutig.
            builder.HasIndex(s => s.EMail).IsUnique();
        }
    }
}