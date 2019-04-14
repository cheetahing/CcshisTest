using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace donetcore.OAuthoriz
{
    public class TokenGenerateOption
    {
        public string Path { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; } 
        public SigningCredentials SigningCredentials { get; set; }
    }
}
