using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Domain.Model
{
    public class EntityBase
    {
        public int Id { get; init; }
        public DateTime? LastChangeDate { get; set; } = DateTime.Now;

        public int? LastChangeUserId { get; set; }
        public User LastChangeUser { get; set; }
    }
}
