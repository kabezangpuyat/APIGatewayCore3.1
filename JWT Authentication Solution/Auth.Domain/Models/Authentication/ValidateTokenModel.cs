using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Models.Authentication
{
    public class ValidateTokenModel : BaseTokenModel
    {
        public string ExpiryDate { get; set; }
    }
}
