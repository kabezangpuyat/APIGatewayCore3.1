using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Models.Authentication
{
    public class RevokeTokenModel
    {
        public string Token { get; set; }
        public string IpAddress { get; set; }
    }
}
