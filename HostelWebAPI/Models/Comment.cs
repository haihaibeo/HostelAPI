using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class Comment
    {
        public Comment()
        {
            ReplyReplyToComments = new HashSet<Reply>();
        }

        [Key]
        [Column("CommentID")]
        [StringLength(50)]
        public string CommentId { get; set; }
        [Required]
        [Column("PropertyID")]
        [StringLength(50)]
        public string PropertyId { get; set; }
        [Required]
        [Column("UserID")]
        [StringLength(450)]
        public string UserId { get; set; }

        [Required]
        [Column("Comment")]
        [StringLength(200)]
        public string CommentString { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime TimeCreated { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? TimeUpdated { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public virtual Property Property { get; set; }

        public virtual Reply ReplyComment { get; set; }

        public virtual ICollection<Reply> ReplyReplyToComments { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
