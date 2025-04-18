﻿using MediatR;
using SB.Domain.Model;
using MediatR;
using SB.Domain.Model;

namespace SB.Application.Commands
{
    public class CreateJobPostingCommand : IRequest<JobPosting>
    {
        public string EmployerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> RequiredSkills { get; set; }
        public string Location { get; set; }
        public string JobType { get; set; }
        public decimal Salary { get; set; }
    }
}





