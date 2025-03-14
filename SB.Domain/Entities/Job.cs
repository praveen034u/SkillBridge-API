using SB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Domain.Entities
{
    public class Job
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public JobStatus Status { get; set; }
        public string EmployerId { get; set; }
        public List<string> RequiredSkills { get; set; } = new();
    }
}
