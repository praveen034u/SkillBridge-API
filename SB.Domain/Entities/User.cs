using SB.Domain.Enums;
using SB.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Domain.Entities
{
        public class User
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
           
            public bool IsActive { get; set; }
            public List<Skill> Skills { get; set; } = new();
            public Roles Role { get; set; }  // Worker, Employer, admin
    }  
}
