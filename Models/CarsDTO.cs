using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ViewApplication.Models
{
    public class CarsDTO
    {
        public int Id { get; set; }
        public string CommonName { get; set; }
        public string name { get; set; }
        public string regNumber { get; set; }
        public string EngineNumber { get; set; }
        public bool IsDeleted { get; set; }         
        public DateTime? ProductionDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? LocationId { get; set; }
        public int? CarManufacturerId { get; set; }
        public int? ApprovedBy { get; set; }
        public decimal? RetailPrice { get; set; }
        public string CarManufacturerName { get; set; }
        public string LocationName { get; set; }
        public string CreatorUserName { get; set; }
        public string LastModifiedBy { get; set; }
        public string DeletedBy { get; set; }     
        public int? ProductionYear { get; set; }      
        public DateTime? DateSold { get; set; }
        public DateTime? DateLeased { get; set; }     
        public string Color { get; set; }
        public CarColor Colors { get; set; }
        public string Condition { get; set; }
        public CarCondition Conditions { get; set; }
        public string CarTypes { get; set; }
        public CarType CarType { get; set; }
        public string Status { get; set; }
        public CarStatus Statuses { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public string Details { get; set; }
        //[Column(TypeName = "decimal(18,2)")]
        public string TotalToPay { get; set; }
       // [Column(TypeName = "decimal(18,2)")]
        public string Amount { get; set; }
        //[Column(TypeName = "decimal(18,2)")]
        public string VAT { get; set; }

        //This is for when the Amount is negotiated from the standard retail price
        public decimal? newAmount { get; set; }
        //Rate

        public decimal? LeasePerHour { get; set; }
        public decimal? LeasePerDay { get; set; }
        public decimal? LeasePerWeek { get; set; }
        public decimal? LeasePerMonth { get; set; }
        public decimal? LeasePerYear { get; set; }
    }
    public class GetTotalResponseDTO
    {
        public string amount { get; set; }
        public string vat { get; set; }
        public string deliveryCharge { get; set; }
        public string total { get; set; }
    }
}
