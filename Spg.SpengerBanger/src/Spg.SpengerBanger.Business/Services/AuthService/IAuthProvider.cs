using Spg.SpengerBanger.Business.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Services.AuthService
{
    public interface IAuthProvider
    {
        (UserInfo userInfo, string errorMessage) CheckUser(string eMail);
    }
}
