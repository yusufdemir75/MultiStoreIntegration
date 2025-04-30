using FluentValidation;
using MultiStoreIntegration.Application.Features.Commands.Sale.Create;
using MultiStoreIntegration.Application.Features.Commands.Sale.Create.Store1CreateSale;

namespace MultiStoreIntegration.Infrastructure.Validations
{
    public class Store1CreateSaleValidation : AbstractValidator<Store1CreateSaleCommandRequest>
    {
        public Store1CreateSaleValidation()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Ürün ID boş olamaz.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Satılacak miktar 0'dan büyük olmalıdır.");
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("Müşteri adı boş olamaz.");
            RuleFor(x => x.PaymentMethod).NotEmpty().WithMessage("Ödeme yöntemi boş olamaz.");
        }
    }
}
