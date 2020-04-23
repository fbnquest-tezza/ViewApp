using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewApplication.Models
{
    public enum CarColor
    {
        Red,
        Blue,
        Green,
        Yellow,
        White,
        Black,
        Grey,
        Cyan,
        Violet,
        Purple,
        Pink,
        Ash
    }
    public enum CarCondition
    {
        New,
        Refurbished,
        Repaired,
        Damaged,
        InTransit,
        InWorshop,
        Stolen,
        Sold
    }
    public enum CarPartsStatus
    {
        New,
        Refurbished,
        InUse,
        InWorkShop,
        Dented,
        Repaired,
        Obsolete
    }
    public enum DeliveryType
    {
        PickUp,
        HomeDelivery
    }
    public enum DurationType
    {
        Second,
        Minute,
        Hour,
        Day,
        Month,
        Year,
        Week
    }
    public enum Gender
    {
        Male,
        Female,
        NotSpecified
    }
    public enum JobCardStatus
    {
        Requested,
        Verified,
        Approved,
        Cancelled
    }
    public enum UserType
    {
        Administrator,
        Employee,
        Partner
    }
    public enum PaymentType
    {
        Full,
        Weekly,
        TwoWeeks,
        Monthly,
        Quarterly,
        HalfYear,
        Annually
    }
    public enum PaymentMethod
    {
        Cash,
        Pos,
        Transfer,
        BankDraft,
        Cheque
    }
    public enum CarType
    {
        Sedan,
        HatchBBack,
        Crossover,
        Coupe,
        Convertible,
        SUV,
        MPV
    }
    public enum CarStatus
    {
        InStock,
        OnLease,
        Sold
    }
}
