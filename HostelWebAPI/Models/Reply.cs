using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class Reply
    {
        [Key]
        [Column("CommentID")]
        [StringLength(50)]
        public string CommentId { get; set; }
        [Required]
        [Column("ReplyToCommentID")]
        [StringLength(50)]
        public string ReplyToCommentId { get; set; }

        [ForeignKey(nameof(CommentId))]
        public virtual Comment Comment { get; set; }

        [ForeignKey(nameof(ReplyToCommentId))]
        public virtual Comment ReplyToComment { get; set; }
    }
}
