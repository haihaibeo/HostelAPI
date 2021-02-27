using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class Owner
    {
        public Owner()
        {
            Property = new HashSet<Property>();
        }

        public string UserId { get; set; }
        public string PassportNumber { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Property> Property { get; set; }
    }
}
