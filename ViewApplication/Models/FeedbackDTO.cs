using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewApplication.Models
{
    public class FeedBackDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public PriorityLevel PriorityLevel { get; set; }
        public string Priority { get; set; }
        public string Reference { get; set; }
        public string Message { get; set; }
        public DateTime CreationTime { get; set; }
        public bool Responded { get; set; }
        public string RepliedMessage { get; set; }
        public string CreatedBy { get; set; }
        public string ModifedBy { get; set; }
        public string DeletedBy { get; set; }
        public string CreatedAt { get; set; }
    }
    public enum PriorityLevel
    {
        Low,
        Normal,
        Medium,
        High
    }
    public class SearchDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? Status { get; set; }
    }
    public class FeedbacksearchDTO
    {
        public string startdate { get; set; }
        public string enddate { get; set; }
        public int? status { get; set; }
    }
}
