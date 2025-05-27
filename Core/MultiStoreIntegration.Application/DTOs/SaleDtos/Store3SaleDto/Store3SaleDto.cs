using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.DTOs.SaleDtos.Store3SaleDto
{
    public class Store3SaleDto
    {
        public string Id { get; set; }
        public float TotalPrice { get; set; }
        public int Quantity { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
