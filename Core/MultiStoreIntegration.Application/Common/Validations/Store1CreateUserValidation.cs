using FluentValidation;
using MultiStoreIntegration.Application.Features.Commands.User.Create.Store1CreateUser;
using MultiStoreIntegration.Application.Repositories.Store1.Store1User;

namespace MultiStoreIntegration.Application.Common.Validations
{
    public class Store1CreateUserValidation : AbstractValidator<Store1CreateUserCommandRequest>
    {
        public Store1CreateUserValidation(Store1IUserReadRepository userReadRepository)
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("İsim alanı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("İsim 100 karakterden uzun olamaz.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.")
                .MustAsync(async (email, cancellation) =>
                {
                    var existingUser = await userReadRepository.GetByEmailAsync(email);
                    return existingUser == null;
                }).WithMessage("Bu email adresiyle zaten bir kullanıcı mevcut.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Rol alanı boş bırakılamaz.")
                .Must(role => role == "Admin" || role == "Moderatör")
                .WithMessage("Rol yalnızca 'Admin' ya da 'Moderatör' olabilir.");
        }
    }
}
