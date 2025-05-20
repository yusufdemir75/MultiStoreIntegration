using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.DTOs.StockDtos.Store2StockDto
{
    public class Store2StockDto
    {
        public Guid Id { get; set; }
        public string? ProductCode { get; set; }
        public string? Category { get; set; }
        public string? ProductName { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
    }
}
