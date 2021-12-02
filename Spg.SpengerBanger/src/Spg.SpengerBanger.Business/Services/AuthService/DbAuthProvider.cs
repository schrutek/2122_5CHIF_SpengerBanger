using Spg.SpengerBanger.Business.Domain.Model;
using Spg.SpengerBanger.Business.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Services.AuthService
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

            User existingUser = _dbContext.Users.SingleOrDefault(u => u.EMail == eMail) ?? new User();
            if (existingUser.Guid != new Guid())
            {
                UserRoles userRole = UserRoles.Customer;
                return (new UserInfo(existingUser.EMail, userRole), message);
            }
            else
            {
                message = "Die Anmeldung ist fehlgeschlagen!";
            }
            return (null, message);
        }
    }
}
