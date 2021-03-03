using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI.Models
{
    public class Owner : User
    {
        public Owner()
        {
            Properties = new HashSet<Property>();
        }
        public string PassportNumber { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
