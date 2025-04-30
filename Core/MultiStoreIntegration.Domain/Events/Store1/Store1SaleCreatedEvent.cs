using MediatR;
using MultiStoreIntegration.Domain.Entities;

namespace MultiStoreIntegration.Domain.Events
{
    public class Store1SaleCreatedEvent : INotification
    {
        public Sale Sale { get; }

        public Store1SaleCreatedEvent(Sale sale)
        {
            Sale = sale;
        }
    }
}
