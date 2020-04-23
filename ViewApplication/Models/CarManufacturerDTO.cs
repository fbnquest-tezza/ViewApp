using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewApplication.Models
{
    public class CarManufacturerDTO
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public long? CreatorUserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CreatorUserName { get; set; }
        public string LastModifiedBy { get; set; }
        public string DeletedBy { get; set; }
    }
}
