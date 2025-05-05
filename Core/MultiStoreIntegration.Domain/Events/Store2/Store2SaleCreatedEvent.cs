using MediatR;
using MultiStoreIntegration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Domain.Events.Store2
{
    public class Store2SaleCreatedEvent : INotification
    {
        public Sale Sale { get; }

        public Store2SaleCreatedEvent(Sale sale)
        {
            Sale = sale;
        }
    }
}
