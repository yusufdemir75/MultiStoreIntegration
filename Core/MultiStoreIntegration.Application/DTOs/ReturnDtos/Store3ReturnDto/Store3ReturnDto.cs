using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.DTOs.ReturnDtos.Store3ReturnDto
{
    public class Store3ReturnDto
    {
        public string? Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public int Quantity { get; set; }
        public string? ReturnReason { get; set; }
    }
}
