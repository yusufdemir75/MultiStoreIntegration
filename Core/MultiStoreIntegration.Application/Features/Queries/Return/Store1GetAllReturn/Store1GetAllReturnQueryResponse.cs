using MultiStoreIntegration.Application.DTOs.ReturnDtos.Store1ReturnDto;
using MultiStoreIntegration.Application.DTOs.SaleDtos.Store1SaleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Queries.Return.Store1GetAllReturn
{
    public class Store1GetAllReturnQueryResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }


        public List<Store1ReturnDto> Store1Returns { get; set; }
    }
}
