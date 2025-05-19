using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MultiStoreIntegration.Application.Features.Commands.Return.Create.Store2CreateReturn;
using MultiStoreIntegration.Application.Features.Commands.Sale.Create.Store3CreateSale;
using MultiStoreIntegration.Application.Repositories.Store2.Store2Return;
using MultiStoreIntegration.Application.Repositories.Store2.Store2Sale;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Return;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Sale;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Stock;
using MultiStoreIntegration.Domain.Events.Store3;
using MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Commands.Return.Create.Store3CreateReturn
{
    public class Store3CreateReturnCommandHandler : IRequestHandler<Store3CreateReturnCommandRequest, Store3CreateReturnCommandResponse>
    {

        private readonly Store3IReturnWriteRepository _store3ReturnWriteRepository;
        private readonly Store3ISaleReadRepository _store3SaleReadRepository;
        private readonly Store3ISaleWriteRepository _store3SaleWriteRepository;
        private readonly Store3IStockReadRepository _store3StockReadRepository;
        private readonly IMediator _mediator;
        private readonly ILogger<Store3CreateReturnCommandHandler> _logger;
        public Store3CreateReturnCommandHandler(Store3IReturnWriteRepository store3ReturnWriteRepository,
            Store3ISaleReadRepository store3SaleReadRepository,
            IMediator mediator,
            ILogger<Store3CreateReturnCommandHandler> logger,
            Store3IStockReadRepository store3IStockReadRepository,
            Store3ISaleWriteRepository store3SaleWriteRepository
            )
        {
            _mediator = mediator;
            _store3ReturnWriteRepository = store3ReturnWriteRepository;
            _store3SaleReadRepository = store3SaleReadRepository;
            _logger = logger;
            _store3StockReadRepository = store3IStockReadRepository;
            _store3SaleWriteRepository = store3SaleWriteRepository;

        }

        public async Task<Store3CreateReturnCommandResponse> Handle(Store3CreateReturnCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var objectId = ObjectId.Parse(request.SaleId);
                var sale = await _store3SaleReadRepository.GetByIdAsync(objectId);

                var stock = await _store3StockReadRepository.GetByIdAsync(sale.ProductId);
                if (sale == null)
                {
                    var msg = $"Satış Bulunamadı.   Ürün ID: {request.SaleId}";
                    _logger.LogWarning(msg);
                    return new Store3CreateReturnCommandResponse
                    {
                        Success = false,
                        Message = msg
                    };
                }

                if (request.Quantity > sale.Quantity)
                {
                    var msg = $"İade Miktariniz Sipariş Miktarından Fazla.   Satış ID: {request.SaleId} ,  Satış Miktarı{request.Quantity} , İade Miktari{sale.Quantity}";
                    _logger.LogWarning(msg);

                    return new Store3CreateReturnCommandResponse
                    {
                        Success = false,
                        Message = msg
                    };
                }

                var store3Return = new Store3ReturnDocument
                {
                    CreatedDate = DateTime.UtcNow,
                    SaleId = objectId,
                    CustomerName = sale.CustomerName,
                    CustomerPhone = sale.CustomerPhone,
                    ProductId = sale.ProductId,
                    Quantity = request.Quantity,
                    ReturnReason = request.ReturnReason,
                    RefundAmount = request.Quantity * stock.UnitPrice,
                };

                var returnResult = await _store3ReturnWriteRepository.AddAsync(store3Return);

                if (!returnResult)
                {
                    var msg = $"İade Kaydı Başarısız.   Satış ID:  {sale.Id} ,  Müşteri Adı:  {sale.CustomerName}";
                    _logger.LogWarning(msg);

                    return new Store3CreateReturnCommandResponse
                    {
                        Success = false,
                        Message = msg
                    };
                }

                sale.Quantity -= request.Quantity;
                sale.TotalPrice = stock.UnitPrice * sale.Quantity;
                sale.UpdatedDate = DateTime.UtcNow;
                await _store3SaleWriteRepository.UpdateAsync(sale);

                await _mediator.Publish(new Store3ReturnCreatedEvent(store3Return), cancellationToken);
                var succesmsg = $"İade Başarıyla oluşturuldu.   Satış ID : {sale.Id}  ,  Müşteri İsmi : {sale.CustomerName}";
                _logger.LogWarning(succesmsg);

                return new Store3CreateReturnCommandResponse
                {
                    Success = true,
                    Message = succesmsg
                };
            }
            catch (Exception ex) { 
                
                var errormsg = $"Beklenmeyen Bir Haa Oluştu : {ex.Message}";
                _logger.LogWarning(errormsg);

                return new Store3CreateReturnCommandResponse
                {
                    Success = false,
                    Message = errormsg
                };
            }

            

            
        }
    }
}
