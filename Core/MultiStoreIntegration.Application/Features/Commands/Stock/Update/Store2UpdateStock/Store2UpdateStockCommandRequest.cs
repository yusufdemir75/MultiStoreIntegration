using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MultiStoreIntegration.Application.Features.Commands.Stock.Update.Store2UpdateStock
{
    public class Store2UpdateStockCommandRequest : IRequest<Store2UpdateStockCommandResponse>
    {
        [Required(ErrorMessage = "Id alanı boş olamaz.")]
        public Guid Id { get; set; }
        public string ProductCode { get; set; }  // Güncellenecek ürünün anahtarı
        public string Category { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
    }
}
