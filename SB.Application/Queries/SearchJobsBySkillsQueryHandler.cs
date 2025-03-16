using MediatR;
using Microsoft.Extensions.Logging;
using SB.Domain.Model;
using SB.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SB.Application.Queries
{
    

    public class SearchJobsBySkillsQueryHandler : IRequestHandler<SearchJobsBySkillsQuery, List<JobPosting>>
    {
        private readonly IJobSearchRepository _jobSearchRepository;
        private readonly ILogger<SearchJobsBySkillsQueryHandler> _logger;

        public SearchJobsBySkillsQueryHandler(IJobSearchRepository jobSearchRepository, ILogger<SearchJobsBySkillsQueryHandler> logger)
        {
            _jobSearchRepository = jobSearchRepository;
            _logger = logger;
        }

        public async Task<List<JobPosting>> Handle(SearchJobsBySkillsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Searching jobs for skills: {Skills}", request.Skills);

            return await _jobSearchRepository.SearchJobsBySkillsAsync(request.Skills);
        }
    }

}
