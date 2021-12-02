using Spg.SpengerBanger.Business.Domain.Model;
using Spg.SpengerBanger.Business.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly SpengerBangerContext _dbContext;

        public UserService(SpengerBangerContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<User> ListTop50()
        {
            return _dbContext.Users.Take(50);
        }
    }
}
