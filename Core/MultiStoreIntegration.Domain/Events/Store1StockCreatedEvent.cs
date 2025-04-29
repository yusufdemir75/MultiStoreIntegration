using MediatR;
using MultiStoreIntegration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Domain.Events
{
    public class Store1StockCreatedEvent : INotification
    {
        public Stock Stock { get; }

        public Store1StockCreatedEvent(Stock stock)
        {
            Stock =stock;
        }
    }
}
