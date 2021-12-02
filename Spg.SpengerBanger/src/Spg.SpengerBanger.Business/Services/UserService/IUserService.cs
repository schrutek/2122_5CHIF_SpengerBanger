﻿using Spg.SpengerBanger.Business.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Services.UserService
{
    public interface IUserService
    {
        IQueryable<User> ListTop50();
    }
}
