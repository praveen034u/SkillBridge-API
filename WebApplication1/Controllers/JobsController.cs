using MediatR;
using Microsoft.AspNetCore.Mvc;
using SB.Application.Queries;
using SB.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SB.API.Controllers;


[ApiController]
[Route("jobs")]
public class JobsController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<JobSearchModel>>> SearchJobs([FromQuery] string query)
    {
        var result = await _mediator.Send(new SearchJobsQuery(query));
        return Ok(result);
    }

    [HttpGet("searchSkill")]
    public async Task<ActionResult<List<JobPosting>>> SearchJobsBySkills([FromQuery] string query)
    {
        var result = await _mediator.Send(new SearchJobsBySkillsQuery(query));
        return Ok(result);
    }
}



