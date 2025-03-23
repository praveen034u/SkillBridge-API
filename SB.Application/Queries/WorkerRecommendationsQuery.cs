using MediatR;

using MediatR;
using System.Collections.Generic;
using SB.Domain.Model;

namespace SB.Application.Queries;



public class WorkerRecommendationsQuery : IRequest<List<Domain.Entities.Profile>>
{
    public string JobId { get; set; }

    public WorkerRecommendationsQuery(string jobId)
    {
        JobId = jobId;
    }
}

