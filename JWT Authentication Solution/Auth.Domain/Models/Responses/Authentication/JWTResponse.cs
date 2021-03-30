using Auth.Domain.Models.Authentication;
using Auth.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Auth.Domain.Models.Responses.Authentication
{
    public class JWTResponse : ICommandQueryResponse
    {
        public string Email { get; set; }
        public string JwtToken { get; set; }
        public DateTime ExpiryDate { get; set; }

        //[JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }
        [JsonIgnore]
        public RefreshTokenModel RefreshTokenModel { get; set; }

        public JWTResponse(string email, string jwtToken, string refreshToken, DateTime expiryDate, RefreshTokenModel refreshModelToken)
        {
            Email = email;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
            ExpiryDate = expiryDate;
            RefreshTokenModel = refreshModelToken;
        }
    }
}
