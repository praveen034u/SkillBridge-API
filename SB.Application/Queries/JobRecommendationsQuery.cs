using MediatR;
using SB.Domain.Model;
using System;
using System.Collections.Generic;
using MediatR;
using System.Collections.Generic;
using SB.Domain.Model;

namespace SB.Application.Queries;

public class JobRecommendationsQuery : IRequest<List<JobPosting>>
{
    public string UserId { get; set; }

    public JobRecommendationsQuery(string userId)
    {
        UserId = userId;
    }
}

