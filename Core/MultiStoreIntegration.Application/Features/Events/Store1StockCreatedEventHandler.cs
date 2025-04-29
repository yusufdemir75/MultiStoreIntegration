using MediatR;
using MultiStoreIntegration.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Events
{
    public class Store1StockCreatedEventHandler : INotificationHandler<Store1StockCreatedEvent>
    {
        public async Task Handle(Store1StockCreatedEvent notification, CancellationToken cancellationToken)
        { 
            

            var stock = notification.Stock;
            Console.WriteLine($"Yeni stok eklendi: {stock.ProductName} - {stock.Quantity} adet");


            await Task.CompletedTask;
        }
    }
}
