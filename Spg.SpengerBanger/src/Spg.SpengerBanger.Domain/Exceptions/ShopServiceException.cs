using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Domain.Exceptions
{
    public class ShopServiceException : Exception
    {
        public ShopServiceException()
            : base()
        { }

        public ShopServiceException(string message)
            : base(message)
        { }

        public ShopServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
