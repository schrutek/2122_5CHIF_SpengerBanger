using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Domain.Model
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime? LastChangeDate { get; set; }

        public int? LastChangeUserId { get; set; }
        public User LastChangeUser { get; set; }
    }
}
