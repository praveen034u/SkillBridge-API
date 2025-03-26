
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace SB.Application;
public class UploadResumeRequest1
{
    [FromForm(Name = "file")]
    public IFormFile File { get; set; }

}
