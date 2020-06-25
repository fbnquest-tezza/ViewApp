using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewApplication.Models
{
    public class RateDTO
    {
        public int? Id { get; set; }
        public int CarId { get; set; }
        public decimal? LeasePerHour { get; set; }
        public decimal? LeasePerDay { get; set; }
        public decimal? LeasePerWeek { get; set; }
        public decimal? LeasePerMonth { get; set; }
        public decimal? LeasePerYear { get; set; }
        public decimal? RetailPrice { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
        public decimal? Amount { get; set; }

        public DateTime? CreationTime { get; set; }

        public string CarName { get; set; }
        public string CarManName { get; set; }
        public string Location { get; set; }
        public string engineNumber { get; set; }
        public string regNumber { get; set; }


        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }

    public class RatePaged
    {
        public RateDTO RateDTO { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
}
