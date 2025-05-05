using MediatR;
using MultiStoreIntegration.Domain.Entities;

namespace MultiStoreIntegration.Domain.Events.Store2
{
    public class Store2ReturnCreatedEvent : INotification
    {
        public Return _Return { get; set; }

        public Store2ReturnCreatedEvent(Return Return )
        {
            _Return = Return;
        }
    }
}
