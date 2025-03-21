using SB.Domain.ValueObjects;

namespace SB.Domain.Entities
{
        public class User
        {
            public string id { get; set; } = Guid.NewGuid().ToString();
            public string categoryId { get; set; } = Guid.NewGuid().ToString();
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }

        public bool isActive { get; set; } = true;
            public List<Skill> skills { get; set; } = new();
            public String role { get; set; }  // Worker, Employer, admin
    }  
}
