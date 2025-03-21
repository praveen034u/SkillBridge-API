using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SB.Domain.Model
{
    public class JobPosting
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [JsonProperty("categoryId")]
        public string CategoryId { get; set; } = Guid.NewGuid().ToString();

        public string EmployerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public List<string> Skills { get; set; }
        public string Skills { get; set; }
        public string Location { get; set; }
        public DateTime? PostedDate { get; set; } = DateTime.UtcNow;
        public string JobType { get; set; } // Full-time, Part-time, Contract
        public decimal Salary { get; set; }
    }
}
