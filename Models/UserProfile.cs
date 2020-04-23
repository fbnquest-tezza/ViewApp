using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewApplication.Models
{
    public class UserProfileModel
    {
        public string NextOfKin { get; set; }
        public string NextOfKinPhone { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Full name")]
        public string Name { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
        public string Referrer { get; set; }
        public string ReferralCode { get; set; }
        public string Address { get; set; }
        public string MiddleName { get; set; }
        public string DateJoined { get; set; }
        public string DateOfBirth { get; set; }
        [Range(0, 199, ErrorMessage = "Age must be between 0 and 199 years")]
        [Display(Name = "Age")]
        public int? Age { get; set; }
        public DateTime DateCreated { get; set; }
        public bool EmailConfirmed { get; set; }
        public string UserTypes { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
    }
}
