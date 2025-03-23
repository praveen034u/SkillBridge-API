//using Azure.Search.Documents;
//using MediatR;
//using Microsoft.Extensions.Options;
//using SB.Application.Services.Interface;
//using SB.Domain;
//using SB.Domain.Entities;
//using SB.Infrastructure.Repositories.Interfaces;

//namespace SB.Application.Queries;

//public class WorkerRecommendationsHandler : IRequestHandler<WorkerRecommendationsQuery, List<Profile>>
//{
//    private readonly SearchClient _searchClient;
//    private readonly IJobPostingRepository _jobPostingRepository;
//    private readonly IUserProfileRepository _userProfileRepository;

//    public WorkerRecommendationsHandler(
//        IOptions<AzureCognitiveSearch> settings,
//        IJobPostingRepository jobPostingRepository,
//        IUserProfileRepository userProfileRepository)
//    {
//        _searchClient = new SearchClient(new Uri(settings.Value.Endpoint), settings.Value.UserIndexName, new Azure.AzureKeyCredential(settings.Value.ApiKey));
//        _jobPostingRepository = jobPostingRepository;
//        _userProfileRepository = userProfileRepository;
//    }

//    //public async Task<List<SB.Domain.Entities.Profile>> Handle(WorkerRecommendationsQuery request, CancellationToken cancellationToken)
//    //{
//    //    var job = await _jobPostingRepository.GetJobAsync(request.JobId);
//    //    if (job == null) return new List<SB.Domain.Entities.Profile>();

//    //    string searchQuery = string.Join(" OR ", job.RequiredSkills);
//    //    var options = new SearchOptions { Size = 10 };
//    //    var response = await _searchClient.SearchAsync<SB.Domain.Entities.Profile>(searchQuery, options);

//    //    List<SB.Domain.Entities.Profile> users = new List<SB.Domain.Entities.Profile>();
//    //    await foreach (var result in response.Value.GetResultsAsync())
//    //    {
//    //        users.Add(result.Document);
//    //    }

//    //    return users;
//    //}
//}

