using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ViewApplication.Models
{
    public class CarSalesDTO
    {
        public string CarName { get; set; }
        public string regNumber { get; set; }
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public string Amount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal RetailPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? VAT { get; set; }
        public string vatString { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPaid { get; set; }
        public string totalPaidString { get; set; }
        public decimal? DeliveryFee { get; set; }
        public string deliveryFeeString { get; set; }
        public int? CarId { get; set; }
        public Guid? CarPartsId { get; set; }
        public int? LocationId { get; set; }
        public int? Quantity { get; set; }
        public string Remark { get; set; }
        public string ApprovedBy { get; set; }
        public int? DivisionId { get; set; }
        public string CreatorUserName { get; set; }
        public string LastModifiedBy { get; set; }
        public string DeletedBy { get; set; }
        public string CustomerAddress { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerMiddleName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public string DeliveryTypes { get; set; }
        public string transactionDate { get; set; }
        public PaymentType PaymentType { get; set; }
        public string PaymentTypes { get; set; }
        public string DateOfBirth { get; set; }
        public DateTime CreationTime { get; set; }
        public Gender Gender { get; set; }
        public string GenderString { get; set; } 
        public string Title { get; set; }
        public string NextOfKinName { get; set; }
        public string NextOfKinPhone { get; set; }
        public string OptionalPhoneNumber { get; set; }
        public string ReferralCode { get; set; }
        public string CarManufacturerName { get; set; }
        public string LocationName { get; set; }
        public string DivisionName { get; set; }
    }
}
