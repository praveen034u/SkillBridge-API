using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Domain.ValueObjects
{
    public record Address(string? Street, string? City, string? State, string? Country);
    public record Name(string FirstName, string? LastName);

    public record Skill(string Name, int ProficiencyLevel);
}
