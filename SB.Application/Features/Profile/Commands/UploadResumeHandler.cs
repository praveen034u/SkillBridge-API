using MediatR;
using SB.Application.Services.Interface;
using SB.Infrastructure.Services;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SB.Application.Features.Profile.Commands;

    public class UploadResumeHandler : IRequestHandler<UploadResumeCommandRequest, string>
    {
        private readonly IBlobStorageService _blobStorageService;

        public UploadResumeHandler(IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        public async Task<string> Handle(UploadResumeCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.File == null || request.File.Length == 0)
                throw new ArgumentException("Invalid file");

            string fileExtension = Path.GetExtension(request.File.FileName);
            string newFileName = $"{Guid.NewGuid()}{fileExtension}";

            using (var stream = request.File.OpenReadStream())
            {
                string fileUrl = await _blobStorageService.UploadFileAsync(stream, newFileName);
                return fileUrl;
            }
        }
    }


