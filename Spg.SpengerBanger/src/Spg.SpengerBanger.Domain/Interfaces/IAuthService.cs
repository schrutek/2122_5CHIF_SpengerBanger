using Spg.SpengerBanger.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Domain.Interfaces
{
    public interface IAuthService
    {
        string Username();

        UserInfo UserInfo();

        Task Login(string eMail);

        Task Logout();

        bool HasRole(string role);
    }
}
