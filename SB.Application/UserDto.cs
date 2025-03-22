using SB.Domain.Enums;

namespace SB.Application
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }

        public string PhoneNumber { get; set; } // Contact Number (Optional)

        public string Address { get; set; }
        public string City { get; set; }

        public string State { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Profile Creation Date
    }

    public class EmployeeUser : User
    {
        public List<string> Skills { get; set; } // List of User Skills
        public string ExperienceLevel { get; set; } // Beginner, Intermediate, Expert
        public List<string> Certifications { get; set; } // Certifications
        public List<string> AppliedJobs { get; set; } // List of Job IDs Applied To
        public byte[] ResumeUrl { get; set; } // Link to Resume (Azure Blob Storage)

        public string LinkedProfileUrl { get; set; } // Contact Number (Optional)
    }

    public class EmployerUser : User
    {
        public string CompanyName { get; set; }

        public string CompanyWebsiteUrl { get; set; }

        public IList<CompanyLocation> CompanyLocations { get; set; }
    }

    public class CompanyLocation
    {
        public string CompanyAddress { get; set; }
        public string CompanyCity { get; set; }

        public string CompanyState { get; set; }
        public string CompanyCountry { get; set; }
    }
}
