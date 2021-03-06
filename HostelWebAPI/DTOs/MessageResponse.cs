using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI
{
    public class MessageResponse
    {
        public MessageResponse(object errors, object messages)
        {
            Errors = errors;
            Messages = messages;
        }
        public object Errors { get; set; }
        public object Messages { get; set; }
    }
}
