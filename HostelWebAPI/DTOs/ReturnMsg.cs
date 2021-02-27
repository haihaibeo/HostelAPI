using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI.DTOs
{
    public class ReturnMsg<T>
    {
        public ReturnMsg(T errors, T messages)
        {
            Errors = errors;
            Messages = messages;
        }
        public T Errors { get; set; }
        public T Messages { get; set; }
    }
}
