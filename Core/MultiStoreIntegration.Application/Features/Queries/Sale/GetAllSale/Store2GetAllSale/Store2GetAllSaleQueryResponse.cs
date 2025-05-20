using MultiStoreIntegration.Application.DTOs.SaleDtos.Store2SaleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Queries.Sale.GetAllSale.Store2GetAllSale
{
    public class Store2GetAllSaleQueryResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }


        public List<Store2SaleDto> Store2Sales { get; set; }
    }
}
