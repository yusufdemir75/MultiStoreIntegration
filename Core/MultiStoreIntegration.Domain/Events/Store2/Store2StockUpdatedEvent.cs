using MediatR;
using MultiStoreIntegration.Domain.Entities;

namespace MultiStoreIntegration.Domain.Events
{
    public class Store2StockUpdatedEvent : INotification
    {
        public Stock Stock { get; }

        public Store2StockUpdatedEvent(Stock stock)
        {
            Stock = stock;
        }
    }
}
