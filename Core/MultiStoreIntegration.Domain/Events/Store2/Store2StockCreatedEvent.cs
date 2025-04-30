using MediatR;
using MultiStoreIntegration.Domain.Entities;

namespace MultiStoreIntegration.Domain.Events
{
    public class Store2StockCreatedEvent : INotification
    {
        public Stock Stock { get; }

        public Store2StockCreatedEvent(Stock stock)
        {
            Stock = stock;
        }
    }
}
