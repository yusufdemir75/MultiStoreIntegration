using MultiStoreIntegration.Application.Features.Commands.Stock.Create.Store3CreateStock;
using MultiStoreIntegration.Application.Repositories.Store3;
using MultiStoreIntegration.Domain.MongoDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Commands.Sale.Create.Store3CreateSale
{
    public class Store3CreateSaleCommandHandler
    {
        private readonly Store3IWriteRepository<SaleDocument> _writeRepository;

        public Store3CreateSaleCommandHandler(Store3IWriteRepository<SaleDocument> writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task<Store3CreateSaleCommandResponse> Handle(Store3CreateSaleCommandRequest request, CancellationToken cancellationToken)
        {
            var newSale = new SaleDocument
            {
                
                
            };

            // Stok verisini MongoDB'ye ekleyelim
            var success = await _writeRepository.AddAsync(newSale);

            // Sonuç döndürelim
            return new Store3CreateSaleCommandResponse
            {
                Success = success,
                Message = success ? "Sale successfully created in Store3 MongoDB." : "Failed to create sale in Store3 MongoDB."
            };
        }
    }
}
