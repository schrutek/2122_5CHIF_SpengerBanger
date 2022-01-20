using Spg.SpengerBanger.Domain.Interfaces;
using Spg.SpengerBanger.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Test.Mock
{
    public class AuthServiceTestMock : IAuthService
    {
        private readonly string _username;

        public AuthServiceTestMock(string username)
        {
            _username = username;
        }

        public bool HasRole(string role)
        {
            throw new NotImplementedException();
        }

        public Task Login(string eMail)
        {
            throw new NotImplementedException();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public UserInfo UserInfo()
        {
            return new(new Guid("582C3776-5726-4349-A6FE-D640671878AB"), "Levin_Hold@hotmail.com", UserRoles.Customer);
        }

        public string Username()
        {
            return _username;
        }
    }
}
