using MultiStoreIntegration.Domain.Entities.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Domain.Entities
{
    public class Sale : BaseEntity
    {
        // Foreign key
        public Guid ProductId { get; set; }
        public Stock Product { get; set; }

        public int Quantity { get; set; }
        public float TotalPrice { get; set; }

        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string PaymentMethod { get; set; }

        // Navigation
        public ICollection<Return> Returns { get; set; }

    }
}
