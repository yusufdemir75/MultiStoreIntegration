using MultiStoreIntegration.Application.DTOs.SaleDtos.Store3SaleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Queries.Sale.GetAllSale.Store3GetAllSale
{
    public class Store3GetAllSaleQueryResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public List<Store3SaleDto> Store3Sales { get; set; }
    }
}
