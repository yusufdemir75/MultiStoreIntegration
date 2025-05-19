using MediatR;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Domain.Events.Store3
{
    public class Store3StockCreatedEvent:INotification
    {
        public Store3StockDocument Stor3StockDocument { get; set; }

        public Store3StockCreatedEvent(Store3StockDocument store3StockDocument)
        {
            Stor3StockDocument = store3StockDocument;
        }
    }
}
