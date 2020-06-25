using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ViewApplication.Models
{
    public class CarLeaseDTO
    {
        public string carName { get; set; }
        public string carManufacturerName { get; set; }
        public string regNumber { get; set; }
        public string engineNumber { get; set; }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerMiddleName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        //Actual Name on receipt
        public string CustomerName { get; set; }
        public string NextOfKinName { get; set; }
        public string NextOfKinPhone { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public string DeliveryTypes { get; set; }
        public PaymentType PaymentType { get; set; }
        public string PaymentTypes { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string PaymentMethods { get; set; }
        public decimal? Amount { get; set; }
        public decimal? TotalPaid { get; set; }
        public decimal? VAT { get; set; }
        public string amountString { get; set; }
        public string vatString { get; set; }
        public string totalPaidString { get; set; }
        public int? CarId { get; set; }
        public Guid? CarPartsId { get; set; }
        public int? LocationId { get; set; }
        public string LocationName { get; set; }
        public int? Quantity { get; set; }
        public string Remark { get; set; }
        public string ApprovedBy { get; set; }
        public int? DivisionId { get; set; }
        public string CreatorUserName { get; set; }
        public string LastModifiedBy { get; set; }
        public string DeletedBy { get; set; }
        public int Duration { get; set; }
        public bool IsDue { get; set; }
        public DurationType DurationType { get; set; }
        public string DurationTypes { get; set; }
        public string PhoneNumber { get; set; }
        public string ExpectedReturnDate { get; set; }
        public int? OverTime { get; set; }
        public string DateOfBirth { get; set; }
        public string Genders { get; set; }
        public Gender Gender { get; set; }
        public string Title { get; set; }
        public string OptionalPhoneNumber { get; set; }
        public string ReferralCode { get; set; }
        public bool IsReturn { get; set; }
        public string CreationTime { get; set; }
    }
    public class PostLeaseDTO
    {
        public string CustomerAddress { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerMiddleName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public Gender Gender { get; set; }
        public string Title { get; set; }
        public string OptionalPhoneNumber { get; set; }
        public string ReferralCode { get; set; }
        public string NextOfKinName { get; set; }
        public string NextOfKinPhone { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public int? CarId { get; set; }
        public string DateOfBirth { get; set; }
        public decimal? Amount { get; set; }
        public decimal? TotalPaid { get; set; }
        public decimal? VAT { get; set; }
        public int Duration { get; set; }
        public bool IsDue { get; set; }
        public DurationType DurationType { get; set; }
        public PaymentType PaymentType { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Remark { get; set; }

    }
}
