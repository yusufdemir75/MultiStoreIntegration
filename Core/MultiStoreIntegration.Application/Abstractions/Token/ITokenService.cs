using MultiStoreIntegration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Abstractions.Token
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
