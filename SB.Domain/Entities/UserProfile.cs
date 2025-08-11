using SB.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Domain.Entities
{
    [Table("user_profiles")] // Match exact table name in Supabase
    public class UserProfile
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        [Column("addressline1")]
        public string Address { get; set; } // Assuming Address is a string, adjust if it's a complex type
        [Column("city")]
        public string City { get; set; }
        [Column("state")]
        public string State { get; set; }
        [Column("country")]
        public string Country { get; set; }
        [Column("zipcode")]
        public string ZipCode { get; set; }
        [Column("dob")]
        public DateOnly DateOfBirth { get; set; }
        [Column("qualification")]
        public string Qualification { get; set; }
        [Column("ssn")]
        public string SSN { get; set; }
        [Column("driving_licence")]
        public string DrivingLicence { get; set; }
        [Column("language_known")]
        public List<string> LanguageKnowns { get; set; }
        [Column("skills")]
        public List<string> Skills { get; set; }
        [Column("experience_years")]
        public int ExperinceOfYear { get; set; }

        [Column("email")]
        public string Email { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("embedding")]
        public float[]? Embedding { get; set; }
    }
}
