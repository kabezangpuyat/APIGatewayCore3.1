using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Auth.Domain.Models.Authentication
{
    public class BaseTokenModel
    {
        [Required]
        public string Token { get; set; }
    }
}
