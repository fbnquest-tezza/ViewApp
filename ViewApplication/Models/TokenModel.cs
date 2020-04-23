using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewApplication.Models
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string RefreshToken { get; set; }
    }


    public class RefreshTokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
