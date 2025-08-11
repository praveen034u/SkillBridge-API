using SB.Domain.Enums;
using SB.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SB.Application
{
    public class User
    {
        [JsonIgnore]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [JsonIgnore]
        public string categoryId { get; set; } = Guid.NewGuid().ToString();
        [JsonIgnore]
        public bool IsActive { get; set; }
        public Name Name { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }

    public class EmployeeUser : User
    {
        public List<Skill> Skills { get; set; } // List of User Skills along with proficiency level i.e. Beginner, Intermediate, Expert                                                
        public List<string> Certifications { get; set; } // Certifications
        public List<string> AppliedJobs { get; set; } // List of Job IDs Applied To
       
        public string ResumeUrl { get; set; } // Link to Resume (Azure Blob Storage)

        public string LinkedInProfileUrl { get; set; } // Contact Number (Optional)
        [JsonIgnore]
        public string Role { get; set; } = "Employee";
    }

    public class EmployerUser : User
    {
        public string CompanyName { get; set; }

        public string CompanyWebsiteUrl { get; set; }

        public List<CompanyLocation> CompanyLocations { get; set; }
        [JsonIgnore]
        public string Role { get; set; } = "Employer";
    }

    public class CompanyLocation
    {
        public string CompanyAddress { get; set; }
        public string CompanyCity { get; set; }

        public string CompanyState { get; set; }
        public string CompanyCountry { get; set; }
    }

    public class UserProfile
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string Address { get; set; } 
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Qualification { get; set; }
        public string SSN { get; set; }
        public string DrivingLicence { get; set; }
        public List<string> LanguageKnowns { get; set; }
        public List<string> Skills { get; set; }
        public int ExperinceOfYear { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
       // [JsonIgnore]
       // public string Embedding { get; set; }
    }
}
