using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SB.Application.Queries;
using SB.Domain.Model;

namespace SB.API.Controllers;




[Route("jobs/recommend")]
[ApiController]
public class JobRecommendationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobRecommendationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<List<JobPosting>>> GetJobRecommendations(string userId)
    {
        var result = await _mediator.Send(new JobRecommendationsQuery(userId));
        return Ok(result);
    }
}

