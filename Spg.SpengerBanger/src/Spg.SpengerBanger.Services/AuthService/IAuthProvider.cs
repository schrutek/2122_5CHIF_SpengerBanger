using Spg.SpengerBanger.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Services.AuthService
{
    public interface IAuthProvider
    {
        (UserInfo userInfo, string errorMessage) CheckUser(string eMail);
    }
}
