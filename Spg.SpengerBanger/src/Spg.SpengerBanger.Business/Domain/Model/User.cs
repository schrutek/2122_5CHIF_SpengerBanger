using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Domain.Model
{
    public enum Gender { NA = 0, FEMALE = 1, MALE = 2 }

    public class User : EntityBase
    {
        public Gender Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }

        public IList<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
    }
}
