using MultiStoreIntegration.Application.DTOs.ReturnDtos.Store3ReturnDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Queries.Return.Store3GetAllReturn
{
    public class Store3GetAllReturnQueryResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public List<Store3ReturnDto> store3Returns { get; set; }
    }
}
