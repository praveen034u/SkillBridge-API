using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SB.Application.Commands;
using SB.Application.Queries;
using SB.Domain.Model;

namespace SB.API.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    public class JobPostingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobPostingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("post")]
        public async Task<IActionResult> PostJob([FromBody] CreateJobPostingCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetJobById(string id)
        //{
        //    var query = new GetJobPostingByIdQuery(id);
        //    var result = await _mediator.Send(query);
        //    return result != null ? Ok(result) : NotFound();
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAllJobs()
        //{
        //    var query = new GetAllJobPostingsQuery();
        //    var result = await _mediator.Send(query);
        //    return Ok(result);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteJob(string id)
        //{
        //    var command = new DeleteJobPostingCommand(id);
        //    await _mediator.Send(command);
        //    return NoContent();
        //}
    }
}





