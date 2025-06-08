using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.DTOs.DailyTotalSale
{
    public class DailyTotalSaleDto
    {
        public string Day { get; set; } = null!; 
        public DateTime Date { get; set; }
        public int TotalProductCount { get; set; }
        public float TotalPrice { get; set; }
    }
}
