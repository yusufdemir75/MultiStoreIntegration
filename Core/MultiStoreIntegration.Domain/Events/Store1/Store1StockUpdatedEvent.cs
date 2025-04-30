using MediatR;
using MultiStoreIntegration.Domain.Entities;

namespace MultiStoreIntegration.Domain.Events.Store1
{
    public class Store1StockUpdatedEvent : INotification
    {
        public Stock Stock { get; }

        public Store1StockUpdatedEvent(Stock stock)
        {
            Stock = stock;
        }
    }
}
