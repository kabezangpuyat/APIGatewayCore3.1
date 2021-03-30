using Auth.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using Auth.Domain.ConfigSections;
using Auth.Domain.Models.Responses.Authentication;
using Auth.Domain.Models.User;
using Auth.Domain.Models.Authentication;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Auth.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Private global method(s)
        private readonly string _secret;
        #endregion

        #region Constructor(s)
        public AuthenticationService(IOptions<AppSettings> appSettings)
        {
            this._secret = appSettings.Value?.Secret ?? string.Empty;
        }
        #endregion

        #region IAuthenticationService
        public JWTResponse Authenticate(string email, string ipAddress)
        {
            // authentication successful so generate jwt and refresh tokens
            var jwtToken = GenerateJwtToken(email);
            var refreshToken = GenerateRefreshToken(ipAddress);

            return new JWTResponse(email, jwtToken, refreshToken.Token, refreshToken.Expires, refreshToken);
        }

        public JWTResponse RefreshToken(string token, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public bool RevokeToken(RevokeTokenModel model)
        {
            throw new NotImplementedException();
        }

        public bool ValidateToken(ValidateTokenModel model)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private method(s)
        private RefreshTokenModel GenerateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshTokenModel
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.Now.AddHours(1),
                    Created = DateTime.Now,
                    CreatedByIp = ipAddress
                };
            }
        }
        private string GenerateJwtToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        //private CurrentUser SetCurrentUser(UserViewModel user)
        //{
        //    CurrentUser currentUser = new CurrentUser();
        //    currentUser.FullName = $"{user.LastName}, {user.FirstName}";
        //    currentUser.ID = user.ID;

        //    return currentUser;
        //}
        //private JsonSerializerOptions DefaultSerializerOptions() =>
        //    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        #endregion
    }
}
