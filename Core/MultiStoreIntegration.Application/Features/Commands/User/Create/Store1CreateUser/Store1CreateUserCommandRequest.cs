using MediatR;

namespace MultiStoreIntegration.Application.Features.Commands.User.Create.Store1CreateUser
{
    public class Store1CreateUserCommandRequest:IRequest<Store1CreateUserCommandResponse>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "Admin";
    }
}
