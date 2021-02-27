using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class Reply
    {
        public string CommentId { get; set; }
        public string ReplyToCommentId { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual Comment ReplyToComment { get; set; }
    }
}
