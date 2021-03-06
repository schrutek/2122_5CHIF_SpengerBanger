using Spg.SpengerBanger.Business.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Domain.Interfaces
{
    public interface IAuthService
    {
        string Username();

        Task Login(string eMail);

        Task Logout();

        bool HasRole(string role);
    }
}
