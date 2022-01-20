using Spg.SpengerBanger.Business.Domain.Model;
using Spg.SpengerBanger.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Services.UserService
{
    public interface IUserService
    {
        IQueryable<User> ListTop50();
    }
}
