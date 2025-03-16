using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.AI.OpenAI;
using Azure.Core;
using Azure.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenAI;

namespace SB.Application.Features.Profile.Commands
{

    public class UploadResumeCommand : IRequest<string>
    {
        public IFormFile ResumeFile { get; set; }
    }

    public class OpenAI
    {
        public string Endpoint { get; set; }
        public string ApiVersion { get; set; }
        public string DeploymentName { get; set; }
    }

    public class UploadResumeCommandHandler : IRequestHandler<UploadResumeCommand, string>
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UploadResumeCommandHandler> _logger;
        private readonly string _endpoint;
        private readonly string _apiVersion;
        private readonly string _deploymentName;
        private readonly TokenCredential _credential;

        public UploadResumeCommandHandler(
            IHttpClientFactory httpClientFactory,
            ILogger<UploadResumeCommandHandler> logger,
            IOptions<OpenAI> openAiOptions)
        {
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;

            var settings = openAiOptions.Value;
            _endpoint = settings.Endpoint;
            _apiVersion = settings.ApiVersion;
            _deploymentName = settings.DeploymentName;
            _credential = new DefaultAzureCredential(); // Entra ID authentication
        }

        public async Task<string> Handle(UploadResumeCommand request, CancellationToken cancellationToken)
        {
            if (request.ResumeFile == null || request.ResumeFile.Length == 0)
                throw new ArgumentException("Invalid resume file.");

            string resumeText = await ExtractTextFromResume(request.ResumeFile);
            return await ProcessResumeWithOpenAI(resumeText);
        }

        private async Task<string> ExtractTextFromResume(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        private async Task<string> ProcessResumeWithOpenAI(string resumeText)
        {
            var chatMessages = new[]
            {
            new { role = "system", content = "Extract skills and experience from the resume text." },
            new { role = "user", content = resumeText }
        };

            var payload = new
            {
                messages = chatMessages,
                max_tokens = 30000,
                temperature = 0.1
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            // ✅ Get access token for Azure OpenAI
            var tokenRequestContext = new TokenRequestContext(new[] { "https://skillbridge-openai.openai.azure.com/openai/deployments/gpt-4o/chat/completions?api-version=2024-10-21" });
           // var accessToken = await _credential.GetTokenAsync(tokenRequestContext, CancellationToken.None);
            var accessToken = "5EoMuawWbr96VAaEJlbhIwONrZvTWFdwLIvAJdeYaMR0XFSL3eUcJQQJ99BCACYeBjFXJ3w3AAABACOG0nXy";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // ✅ Construct the correct Azure OpenAI URL dynamically
            var openAiUrl = $"https://skillbridge-openai.openai.azure.com/openai/deployments/gpt-4o/chat/completions?api-version=2024-10-21";

            var response = await _httpClient.PostAsync(openAiUrl, requestContent);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError("Error calling OpenAI API: {Error}", error);
                throw new Exception($"OpenAI API error: {response.StatusCode}");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(responseBody);
            return jsonDoc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
        }
    }
}