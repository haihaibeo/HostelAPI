using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI
{
    public partial class MessageResponse
    {
        public MessageResponse(object errors, object messages)
        {
            Errors = errors;
            Messages = messages;
        }
        public object Errors { get; set; }
        public object Messages { get; set; }
    }
           
    public partial class TokenResponse
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
