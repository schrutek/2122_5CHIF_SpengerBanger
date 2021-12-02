using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Domain.Model
{
    public enum UserRoles { Customer, Administrator }

    public record UserInfo(
        string EMail,
        UserRoles UserRole
        );
}
