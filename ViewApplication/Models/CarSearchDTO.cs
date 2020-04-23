using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewApplication.Models
{
    public class CarSearchDTO
    {
        public decimal? leastAmount { get; set; }
        public decimal? maxAmount { get; set; }
        public int? locationId { get; set; }
        public int? prodYear { get; set; }
        public DateTime? ProductionDate { get; set; }
        public string color { get; set; }
        public CarType? carType { get; set; }
        public int? carManufacturerId { get; set; }
        public string status { get; set; }
        public string condition { get; set; }
        public CarColor? Colors { get; set; }
        public CarCondition? Conditions { get; set; }
        public CarStatus? Statuses { get; set; }
    }
}
