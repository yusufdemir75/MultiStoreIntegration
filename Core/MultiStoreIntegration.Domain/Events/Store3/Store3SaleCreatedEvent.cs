using MediatR;
using MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Domain.Events.Store3
{
    public class Store3SaleCreatedEvent : INotification
    {
        public Store3SaleDocument Store3SaleDocument { get; set; }

        public Store3SaleCreatedEvent(Store3SaleDocument store3SaleDocument)
        {
            Store3SaleDocument = store3SaleDocument;
        }
    }
}
