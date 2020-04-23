using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ViewApplication.Models.Country;

namespace ViewApplication.Models
{
    public class LocationDTO
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public eCountry Country { get; set; }
        public string CountryName { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }
        public int LocationId { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationTime { get; set; }
        public string CreatorUserName { get; set; }
        public string LastModifiedBy { get; set; }
        public string DeletedBy { get; set; }
    }
}
