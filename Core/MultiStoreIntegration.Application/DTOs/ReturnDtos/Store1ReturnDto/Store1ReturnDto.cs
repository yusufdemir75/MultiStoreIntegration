using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.DTOs.ReturnDtos.Store1ReturnDto
{
    public class Store1ReturnDto
    {
        public Guid Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public int Quantity { get; set; }
        public string? ReturnReason { get; set; }
    }
}
