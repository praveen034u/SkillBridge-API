using MediatR;
using SB.Domain.Model;
using System.Collections.Generic;


namespace SB.Application.Queries
{
    
    public class SearchJobsBySkillsQuery : IRequest<List<JobPosting>>
    {
        public string Skills { get; set; }

        public SearchJobsBySkillsQuery(string skills)
        {
            Skills = skills;
        }
    }

}
