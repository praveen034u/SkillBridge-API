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
        public List<string> RequiredSkills { get; set; } = new(); // store JSONB as string in EF
        public string Location { get; set; }
        public string EmploymentType { get; set; }
        public SalaryRange SalaryRange { get; set; } = new(); // JSONB as string
        public int? ExperienceRequired { get; set; }
        public DateTime? ApplicationDeadline { get; set; }
        public string Status { get; set; } = "open";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class SalaryRange
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public string Currency { get; set; } = string.Empty;
    }
}
