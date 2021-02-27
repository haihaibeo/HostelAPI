using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HostelWebAPI.Models
{
    public partial class PropertyAddress
    {
        public string PropertyId { get; set; }
        public string CityId { get; set; }
        public string StreetName { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }

        public virtual City City { get; set; }
        public virtual Property Property { get; set; }
    }
}
