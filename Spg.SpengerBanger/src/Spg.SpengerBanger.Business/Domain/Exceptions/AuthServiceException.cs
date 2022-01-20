﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Business.Domain.Exceptions
{
    public class AuthServiceException : Exception
    {
        public AuthServiceException()
            : base()
        { }

        public AuthServiceException(string message)
            : base(message)
        { }

        public AuthServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

