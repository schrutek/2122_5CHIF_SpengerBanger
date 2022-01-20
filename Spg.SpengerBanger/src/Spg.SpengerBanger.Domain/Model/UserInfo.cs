using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Domain.Model
{
    public enum UserRoles { Customer, Administrator }

    public record UserInfo(
        Guid ActiveShoppingCartId,
        string EMail,
        UserRoles UserRole
        );
}
