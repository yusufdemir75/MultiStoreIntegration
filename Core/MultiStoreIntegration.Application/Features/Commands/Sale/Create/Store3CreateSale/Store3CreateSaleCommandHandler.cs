using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Sale;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Stock;
using MultiStoreIntegration.Domain.Events.Store3;
using MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Commands.Sale.Create.Store3CreateSale
{
    public class Store3CreateSaleCommandHandler : IRequestHandler<Store3CreateSaleCommandRequest, Store3CreateSaleCommandResponse>
    {
        private readonly Store3ISaleWriteRepository _saleWriteRepository;
        private readonly Store3IStockWriteRepository _stockWriteRepository;
        private readonly Store3IStockReadRepository _stockReadRepository;
        private readonly IMediator _mediator;
        private readonly ILogger<Store3CreateSaleCommandHandler> _logger;

        public Store3CreateSaleCommandHandler(
            Store3ISaleWriteRepository saleWriteRepository,
            Store3IStockReadRepository stockReadRepository,
            Store3IStockWriteRepository stockWriteRepository,
            IMediator mediator,
            ILogger<Store3CreateSaleCommandHandler> logger)
        {
            _saleWriteRepository = saleWriteRepository;
            _stockReadRepository = stockReadRepository;
            _stockWriteRepository = stockWriteRepository;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<Store3CreateSaleCommandResponse> Handle(Store3CreateSaleCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var objectId = ObjectId.Parse(request.ProductId);
                var stock = await _stockReadRepository.GetByIdAsync(objectId);

                if (stock == null)
                {
                    var msg = $"Stok bulunamadı. Ürün ID: {request.ProductId}";
                    _logger.LogWarning(msg);
                    return new Store3CreateSaleCommandResponse
                    {
                        Success = false,
                        Message = msg
                    };
                }

                if (stock.Quantity < request.Quantity)
                {
                    var msg = $"Stok yetersiz. Ürün ID: {request.ProductId}, Mevcut: {stock.Quantity}, Talep edilen: {request.Quantity}";
                    _logger.LogWarning(msg);
                    return new Store3CreateSaleCommandResponse
                    {
                        Success = false,
                        Message = msg
                    };
                }

                var sale = new Store3SaleDocument
                {
                    ProductId = objectId,
                    Quantity = request.Quantity,
                    TotalPrice = request.Quantity * stock.UnitPrice,
                    CustomerName = request.CustomerName,
                    CustomerPhone = request.CustomerPhone,
                    PaymentMethod = request.PaymentMethod,
                    CreatedDate = DateTime.UtcNow
                };

                var saleResult = await _saleWriteRepository.AddAsync(sale);

                if (!saleResult)
                {
                    var msg = $"Satış kaydı başarısız. Ürün ID: {request.ProductId}, Müşteri: {request.CustomerName}";
                    _logger.LogError(msg);
                    return new Store3CreateSaleCommandResponse
                    {
                        Success = false,
                        Message = msg
                    };
                }

                stock.Quantity -= request.Quantity;
                stock.UpdatedDate = DateTime.UtcNow;
                await _stockWriteRepository.UpdateAsync(stock);
                await _mediator.Publish(new Store3SaleCreatedEvent(sale), cancellationToken);

                var successMsg = $"Satış başarıyla kaydedildi. Ürün ID: {request.ProductId}, Adet: {request.Quantity}, Müşteri: {request.CustomerName}";
                _logger.LogInformation(successMsg);

                return new Store3CreateSaleCommandResponse
                {
                    Success = true,
                    Message = successMsg
                };
            }
            catch (Exception ex)
            {
                var errorMsg = $"Beklenmeyen bir hata oluştu: {ex.Message}";
                _logger.LogError(ex, "Satış işlemi sırasında hata oluştu.");
                return new Store3CreateSaleCommandResponse
                {
                    Success = false,
                    Message = errorMsg
                };
            }
        }
    }
}
