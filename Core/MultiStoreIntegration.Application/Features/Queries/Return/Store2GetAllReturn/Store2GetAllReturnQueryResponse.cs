using MultiStoreIntegration.Application.DTOs.ReturnDtos.Store2ReturnDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Queries.Return.Store2GetAllReturn
{
    public class Store2GetAllReturnQueryResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public List<Store2ReturnDto> Store2Returns { get; set; }
    }
}
