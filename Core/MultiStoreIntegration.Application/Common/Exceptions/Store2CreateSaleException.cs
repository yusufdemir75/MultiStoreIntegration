using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Common.Exceptions
{
    public class Store2CreateSaleException : Exception
    {
        public Store2CreateSaleException(string message) : base(message)
        {
            
        }
    }
}
