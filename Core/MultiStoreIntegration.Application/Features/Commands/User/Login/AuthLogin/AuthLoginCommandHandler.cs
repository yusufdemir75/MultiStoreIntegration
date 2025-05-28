using MediatR;
using Microsoft.AspNetCore.Identity;
using MultiStoreIntegration.Application.Abstractions.Token;
using MultiStoreIntegration.Application.Repositories.Store1.Store1User;
namespace MultiStoreIntegration.Application.Features.Commands.User.Login.AuthLogin
{
    public class AuthLoginCommandHandler : IRequestHandler<AuthLoginCommandRequest, AuthLoginCommandResponse>
    {
        private readonly Store1IUserReadRepository _userReadRepository;
        private readonly ITokenService _tokenService;
        private readonly PasswordHasher<Domain.Entities.User> _passwordHasher;

        public AuthLoginCommandHandler(Store1IUserReadRepository userReadRepository, ITokenService tokenService)
        {
            _userReadRepository = userReadRepository;
            _tokenService = tokenService;
            _passwordHasher = new PasswordHasher<Domain.Entities.User>();
        }

        public async Task<AuthLoginCommandResponse> Handle(AuthLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userReadRepository.GetSingleAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return new AuthLoginCommandResponse
                {
                    Message = "Kullanıcı bulunamadı."
                };
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return new AuthLoginCommandResponse
                {
                    Message = "Şifre hatalı."
                };
            }

            var token = _tokenService.GenerateToken(user);

            return new AuthLoginCommandResponse
            {
                Token = token,
                Email = user.Email,
                Role = user.Role,
                Message = "Giriş başarılı."
            };
        }
    }

}
