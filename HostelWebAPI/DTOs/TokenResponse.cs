using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI
{
    public class TokenResponse
    {
        public TokenResponse(string userId, string token, string userName)
        {
            UserId = userId;
            Token = token;
            UserName = userName;
        }

        public TokenResponse(string userId, string token)
        {
            UserId = userId;
            Token = token;
        }

        public string UserId { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
    }
}
