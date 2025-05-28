using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MultiStoreIntegration.Application.Repositories.Store1.Store1User;

namespace MultiStoreIntegration.Application.Features.Commands.User.Create.Store1CreateUser
{
    public class Store1CreateUserCommandHandler : IRequestHandler<Store1CreateUserCommandRequest, Store1CreateUserCommandResponse>
    {
        private readonly Store1IUserReadRepository _userReadRepository;
        private readonly Store1IUserWriteRepository _userWriteRepository;
        private readonly IValidator<Store1CreateUserCommandRequest> _validator;
        private readonly PasswordHasher<Domain.Entities.User> _passwordHasher;

        public Store1CreateUserCommandHandler(
            Store1IUserReadRepository userReadRepository,
            Store1IUserWriteRepository userWriteRepository,
            IValidator<Store1CreateUserCommandRequest> validator)
        {
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
            _validator = validator;
            _passwordHasher = new PasswordHasher<Domain.Entities.User>();
        }

        public async Task<Store1CreateUserCommandResponse> Handle(Store1CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                string errorMessages = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                return new Store1CreateUserCommandResponse
                {
                    Success = false,
                    Message = errorMessages
                };
            }

            var user = new Domain.Entities.User
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                Role = request.Role,
                CreatedDate = DateTime.UtcNow
            };

            user.Password = _passwordHasher.HashPassword(user, request.Password);

            await _userWriteRepository.AddAsync(user);
            await _userWriteRepository.SaveAsync();

            return new Store1CreateUserCommandResponse
            {
                Success = true,
                Message = "Kullanıcı başarıyla oluşturuldu."
            };
        }
    }
}
