using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SB.Domain.Model
{
    public class JobPosting
    {
        public Guid JobId { get; set; }
        public Guid EmployerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RequiredSkills { get; set; } // store JSONB as string in EF
        public string Location { get; set; }
        public string EmploymentType { get; set; }
        public string SalaryRange { get; set; } // JSONB as string
        public int? ExperienceRequired { get; set; }
        public DateTime? ApplicationDeadline { get; set; }
        public string Status { get; set; } = "open";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
