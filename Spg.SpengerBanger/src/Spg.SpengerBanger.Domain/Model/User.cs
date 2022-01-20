using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Domain.Model
{
    public enum Gender { NA = 0, FEMALE = 1, MALE = 2 }

    public class User : EntityBase
    {
        protected User() { }
        public User(Gender gender, string firstName, string lastName, string eMail, Guid guid, Address address)
        {
            Gender = gender;
            FirstName = firstName;
            LastName = lastName;
            EMail = eMail;
            Guid = guid;
            Address = address;
        }
        public Gender Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public Guid Guid { get; set; }
        public Address Address { get; set; }


        protected List<ShoppingCart> _shoppingCarts = new();
        public virtual IReadOnlyList<ShoppingCart> ShoppingCarts => _shoppingCarts;

        /// <summary>
        /// Gibt den aktiven Warenkorb zurück, oder erstellt einen neuen aktiven Warenkorb.
        /// </summary>
        /// <returns></returns>
        public ShoppingCart GetActiveShoppingCartOrNew()
        {
            ShoppingCart activeShoppingCart = _shoppingCarts
                .SingleOrDefault(s => s.State == States.Active);
            activeShoppingCart.LastChangeDate = DateTime.Now;
            if (activeShoppingCart is null)
            {
                activeShoppingCart = new ShoppingCart(Guid.NewGuid(), this) 
                {
                    State = States.Active 
                };
                _shoppingCarts.Add(activeShoppingCart);
            }
            return activeShoppingCart;
        }

        public static User CreateEmpty()
        {
            return new User();
        }
    }
}
