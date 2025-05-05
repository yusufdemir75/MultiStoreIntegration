using MediatR;
using MultiStoreIntegration.Domain.Entities;


namespace MultiStoreIntegration.Domain.Events.Store1
{
    public class Store1ReturnCreatedEvent : INotification
    {
        public Return _Return { get; }

        public Store1ReturnCreatedEvent(Return Return)
        {
            _Return = Return;
        }
    }
}
