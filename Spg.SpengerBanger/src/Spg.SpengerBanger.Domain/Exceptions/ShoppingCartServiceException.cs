using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Domain.Exceptions
{
    public class ShoppingCartServiceException : Exception
    {
        public ShoppingCartServiceException()
            : base()
        { }

        public ShoppingCartServiceException(string message)
            : base(message)
        { }

        public ShoppingCartServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
