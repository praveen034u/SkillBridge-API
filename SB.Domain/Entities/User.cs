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
            public string id { get; set; }
            public string categoryId { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }
           
            public bool isActive { get; set; }
            public List<Skill> skills { get; set; } = new();
            public String role { get; set; }  // Worker, Employer, admin
    }  
}
