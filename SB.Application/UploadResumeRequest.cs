
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace SB.Application;
public class UploadResumeRequest
{
    [FromForm(Name = "file")]
    public IFormFile ResumeFile { get; set; }

    [FromForm(Name = "userId")]
    public string UserId { get; set; }
}
