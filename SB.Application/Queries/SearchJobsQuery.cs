using MediatR;
using SB.Domain.Model;
using System.Collections.Generic;

namespace SB.Application.Queries
{


    public class SearchJobsQuery : IRequest<List<JobSearchModel>>
    {
        public string Query { get; set; }

        public SearchJobsQuery(string query)
        {
            Query = query;
        }
    }

}
