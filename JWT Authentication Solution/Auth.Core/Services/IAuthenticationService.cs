using Auth.Domain.Models.Authentication;
using Auth.Domain.Models.Responses.Authentication;
using Auth.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Core.Services
{
    public interface IAuthenticationService
    {
        JWTResponse Authenticate(string email, string ipAddress);
        JWTResponse RefreshToken(string token, string ipAddress);
        bool RevokeToken(RevokeTokenModel model);
        bool ValidateToken(ValidateTokenModel model);
    }
}
