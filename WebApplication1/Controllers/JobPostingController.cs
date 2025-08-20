using MediatR;
using Microsoft.AspNetCore.Mvc;
using SB.Application.JobPostings.Commands;
using SB.Application.JobPostings.Queries;
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

        //[HttpPost("post")]
        //public async Task<IActionResult> PostJob([FromBody] CreateJobPostingCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return Ok(result);
        //}

        // GET: api/jobposting
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobs = await _mediator.Send(new GetJobPostingsQuery());
            return Ok(jobs);
        }

        // GET: api/jobposting/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var job = await _mediator.Send(new GetJobPostingByIdQuery(id));
            if (job == null) return NotFound();
            return Ok(job);
        }

        // POST: api/jobposting
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobPostingCommand command)
        {
            var jobId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = jobId }, new { jobId });
        }

        // PUT: api/jobposting/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateJobPostingCommand command)
        {
            if (id != command.JobId) return BadRequest("Job ID mismatch.");

            var updated = await _mediator.Send(command);
            if (!updated) return NotFound();

            return NoContent();
        }

        // DELETE: api/jobposting/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _mediator.Send(new DeleteJobPostingCommand(id));
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}





