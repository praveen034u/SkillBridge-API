using SB.Domain.ValueObjects;
using System.Runtime.InteropServices.JavaScript;

namespace SB.Domain.Entities
{
        public class User
        {
            public string id { get; set; } = Guid.NewGuid().ToString();
          
            public JSObject userProfile { get; set; }

            public bool isActive { get; set; } = true;
         
            public String role { get; set; }  // Worker, Employer, admin
    }  
}
