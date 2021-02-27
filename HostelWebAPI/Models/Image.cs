using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class Image
    {
        public string ImageId { get; set; }
        public string Url { get; set; }
        public string PropertyId { get; set; }
        public string Alt { get; set; }

        public virtual Property Property { get; set; }
    }
}
