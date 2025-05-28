using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Commands.User.Login.AuthLogin
{
    public class AuthLoginCommandRequest :IRequest<AuthLoginCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
