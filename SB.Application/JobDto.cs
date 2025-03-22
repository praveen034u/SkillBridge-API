using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application
{
    internal class JobDto
    {
        public string Id { get; set; } // Unique Job ID
        public string Title { get; set; } // Job Title
        public string Description { get; set; } // Job Description
        public List<string> RequiredSkills { get; set; } // List of Required Skills
        public string CompanyName { get; set; } // Name of the Company
        public string Location { get; set; } // Job Location (Remote/City)
        public string EmploymentType { get; set; } // Full-Time, Part-Time, Internship
        public decimal Salary { get; set; } // Salary (optional)
        public string PostedByUserId { get; set; } // User ID of Job Poster
        public DateTime PostedDate { get; set; } = DateTime.UtcNow; // Job Posting Date
        public bool IsActive { get; set; } = true; // Is Job Active?
    }
}
