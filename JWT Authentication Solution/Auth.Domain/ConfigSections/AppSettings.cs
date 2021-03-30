using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.ConfigSections
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public bool SeedData { get; set; }
    }
}
