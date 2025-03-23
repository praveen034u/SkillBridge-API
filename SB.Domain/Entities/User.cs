using SB.Domain.ValueObjects;
using System.Runtime.InteropServices.JavaScript;

namespace SB.Domain.Entities
{
        public class User
        {
            public string id { get; set; } = Guid.NewGuid().ToString();
            public string categoryId { get; set; } = Guid.NewGuid().ToString();
          
            public string userProfile { get; set; } // json format 

            public bool isActive { get; set; } = true;
         
            public String role { get; set; }  // Worker, Employer, admin
    }  
}
