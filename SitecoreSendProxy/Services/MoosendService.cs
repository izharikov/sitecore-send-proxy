using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RestSharp;
using SitecoreSendProxy.Options;

namespace SitecoreSendProxy.Services
{
    public class SitecoreSendService: ISitecoreSendService
    {
        private readonly MoosendSettings _moosendSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        
        public SitecoreSendService(IOptions<MoosendSettings> settings, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _moosendSettings = settings.Value;
        }

        public async Task<bool> IsSubscriberExists(string email)
        {
            var client = new RestClient(_httpClientFactory.CreateClient(Constants.SitecoreSendClient));
            var request = new RestRequest(string.Format(_moosendSettings.SubscribersSearchEndpoint,
                _moosendSettings.SubscribersId, _moosendSettings.ApiKey, email));
            var response = await client.GetAsync<FindSubscriberResponse>(request);
            return response.Code == 0;
        }

        public async Task<AddTagResponse> AddUserTag(string email, string tag)
        {
            string[] tags = new string[1] { tag };
            if (string.IsNullOrEmpty(tag))
                tags = new string[] { };

            var client = new RestClient(_httpClientFactory.CreateClient(Constants.SitecoreSendClient));
            var request = new RestRequest(string.Format(_moosendSettings.SubscribersEndpoint, _moosendSettings.SubscribersId, _moosendSettings.ApiKey), Method.Post);
            request.AddParameter("application/json", new { Email = email, Tags = tags }, ParameterType.RequestBody);
            var response = await client.PostAsync<AddTagResponse>(request);
            return response;
        }
    }

    public interface ISitecoreSendService
    {
        Task<AddTagResponse> AddUserTag(string email, string tag);
        Task<bool> IsSubscriberExists(string email);
    }
    public class AddTagResponse
    {
        public int Code { get; set; }
        public object Error { get; set; }
        public Context Context { get; set; }
        
    }

    public class FindSubscriberResponse
    {
        public int Code { get; set; }
    }
    public class Context
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public List<string> Tags { get; set; }
    }
}
