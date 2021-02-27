using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class PropertyType
    {
        public PropertyType()
        {
            Property = new HashSet<Property>();
        }

        public string PropertyTypeId { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Property> Property { get; set; }
    }
}
