using MultiStoreIntegration.Domain.Entities.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Domain.Entities
{
    public class Stock:BaseEntity
    {
        public string ProductCode { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }

        // Navigation properties
        public ICollection<Sale> Sales { get; set; }
        public ICollection<Return> Returns { get; set; }
    }
}
