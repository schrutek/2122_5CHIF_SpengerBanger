using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Spg.SpengerBanger.Domain.Model
{
    public enum States { Active, InPayment, Sent }

    public class ShoppingCart : EntityBase
    {
        protected ShoppingCart() { }
        public ShoppingCart(Guid guid, User user)
        {
            Guid = guid;
            UserNavigation = user;
            State = States.Active;
        }


        public States State { get; set; }
        public Guid Guid { get; set; }


        public int UserId { get; set; }
        public virtual User UserNavigation { get; private set; }


        private List<ShoppingCartItem> _shoppingCartItems = new();
        public virtual IReadOnlyList<ShoppingCartItem> ShoppingCartItems => _shoppingCartItems;


        public void AddItem(ShoppingCartItem shoppingCartItem)
        {
            if (shoppingCartItem is not null)
            {
                shoppingCartItem.ShoppingCartId = Id;
                shoppingCartItem.LastChangeDate = DateTime.Now;
                shoppingCartItem.LastChangeUserId = UserNavigation.Id;
                shoppingCartItem.LastChangeUser = UserNavigation;

                var existingItem = ShoppingCartItems.FirstOrDefault(s => s.ProductId == shoppingCartItem.ProductId);
                shoppingCartItem.ProductNavigation.Stock -= shoppingCartItem.Pieces;    // Unittest notwendig
                if (existingItem is not null)
                {
                    existingItem.Pieces += shoppingCartItem.Pieces;                     // Unittest notwendig
                }
                else
                {
                    _shoppingCartItems.Add(shoppingCartItem);                           // Unittest notwendig
                }
            }
        }

        public void RemoveItem(int productId, int pieces)
        {
            var existingItem = ShoppingCartItems.FirstOrDefault(s => s.ProductId == productId);
            if (existingItem is not null)
            {
                existingItem.LastChangeDate = DateTime.Now;
                existingItem.LastChangeUserId = UserNavigation.Id;
                existingItem.LastChangeUser = UserNavigation;

                existingItem.ProductNavigation.Stock += pieces;                     // Unittest notwendig
                existingItem.Pieces -= pieces;                                      // Unittest notwendig
                if (existingItem.Pieces <= 0)
                {
                    _shoppingCartItems.Remove(existingItem);                        // Unittest notwendig
                }
            }
        }
    }
}
