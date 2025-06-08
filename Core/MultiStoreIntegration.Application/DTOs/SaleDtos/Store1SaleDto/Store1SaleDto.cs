using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.DTOs.SaleDtos.Store1SaleDto
{
    public class Store1SaleDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }

        public string? Category { get; set; }
        public string? ProductName { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public DateTime UpdatedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
