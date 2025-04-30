using MediatR;
using MultiStoreIntegration.Domain.Entities;

namespace MultiStoreIntegration.Domain.Events.Store1
{
    public class Store1StockCreatedEvent : INotification
    {
        public Stock Stock { get; }

        public Store1StockCreatedEvent(Stock stock)
        {
            Stock = stock;
        }
    }
}
