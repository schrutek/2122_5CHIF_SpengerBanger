using Microsoft.EntityFrameworkCore;
using Spg.SpengerBanger.Domain.Model;
using Spg.SpengerBanger.Infrastructure;
using System;
using System.Linq;

namespace Spg.SpengerBanger.Services.AuthService
{
    public class DbAuthProvider : IAuthProvider
    {
        private readonly SpengerBangerContext _dbContext;

        public DbAuthProvider(SpengerBangerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public (UserInfo userInfo, string errorMessage) CheckUser(string eMail)
        {
            string message = string.Empty;

            User existingUser = _dbContext
                .Users
                .Include(c => c.ShoppingCarts)
                .SingleOrDefault(u => u.EMail == eMail)
                    ?? User.CreateEmpty();

            if (existingUser.Guid != new Guid())
            {
                ShoppingCart activeShoppingCart = existingUser.GetActiveShoppingCartOrNew();
                UserRoles userRole = UserRoles.Customer;
                return (new UserInfo(activeShoppingCart.Guid, existingUser.EMail, userRole), message);
            }
            else
            {
                message = "Die Anmeldung ist fehlgeschlagen!";
            }
            return (null, message);
        }
    }
}
