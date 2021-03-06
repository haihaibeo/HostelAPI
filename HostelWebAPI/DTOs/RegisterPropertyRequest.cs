using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelWebAPI
{
    public class RegisterPropertyRequest
    {
        public string Name { get; set; }
        public string Discription { get; set; }
        public string PropertyTypeId { get; set; }
        public IEnumerable<string> Images { get; set; }
        public string PricePerNight { get; set; }
        public string CityId { get; set; }
        public IEnumerable<string> ServiceIds { get; set; }
    }

    public class PropertyResponseViewResponse
    {

    }
}
