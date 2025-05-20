using MultiStoreIntegration.Application.DTOs.SaleDtos.Store1SaleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Queries.Sale.GetAllSale.Store1GetAllSale
{
    public class Store1GetAllSaleQueryResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }


        public List<Store1SaleDto> Store1Sales { get; set; }
    }
}
