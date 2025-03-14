using SB.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SB.Domain.Entities
{
    public class Profile
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<string> Skills { get; set; }
        public List<string> Education { get; set; }
        public List<string> WorkExperience { get; set; }
    }
}
