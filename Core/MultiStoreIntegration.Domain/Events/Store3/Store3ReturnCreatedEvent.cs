using MediatR;
using MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Domain.Events.Store3
{
    public class Store3ReturnCreatedEvent:INotification
    {
        public Store3ReturnDocument Store3ReturnDocument { get; set; }

        public Store3ReturnCreatedEvent(Store3ReturnDocument store3ReturnDocument)
        {
            Store3ReturnDocument = store3ReturnDocument;
        }
    }
}
