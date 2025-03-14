using SB.Domain.Enums;

namespace SB.Application
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
        public bool IsActive { get; set; }
    }
}
