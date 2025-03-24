using MediatR;
using Microsoft.AspNetCore.Mvc;
using SB.Application;


namespace SB.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[HttpPost("upload-resume")]
    //[Consumes("multipart/form-data")]
    //public async Task<IActionResult> UploadResume([FromForm] UploadResumeRequest request)
    //{
    //    // Now pass the file and userId to MediatR Command
    //    var command = new SB.Application.Features.Profile.Commands.UploadResumeCommand
    //    {
    //        ResumeFile = request.ResumeFile,
    //        //UserId = request.UserId
    //    };

    //    var profileId = await _mediator.Send(command);
    //    return Ok(new { ProfileId = profileId });
    //}

}