using System;

namespace MultiStoreIntegration.Infrastructure.Exceptions
{
    public class Store1CreateSaleException : Exception
    {
        public Store1CreateSaleException(string message) : base(message)
        {
        }
    }
}
