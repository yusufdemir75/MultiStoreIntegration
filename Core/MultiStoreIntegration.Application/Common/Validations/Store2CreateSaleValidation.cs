using FluentValidation;
using MultiStoreIntegration.Application.Features.Commands.Sale.Create.Store2CreateSale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Common.Validations
{
    public class Store2CreateSaleValidation : AbstractValidator<Store2CreateSaleCommandRequest>
    {
        public Store2CreateSaleValidation()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Ürün ID boş olamaz.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Satılacak miktar 0'dan büyük olmalıdır.");
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("Müşteri adı boş olamaz.");
            RuleFor(x => x.PaymentMethod).NotEmpty().WithMessage("Ödeme yöntemi boş olamaz.");

        }
        
    }
}
