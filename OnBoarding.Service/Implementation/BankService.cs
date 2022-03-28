using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Onboarding.Core.Config;
using OnBoarding.Core.Responses;
using OnBoarding.Service.Interface;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OnBoarding.Service.Implementation
{
    public class BankService : IBankService
    {
        //private readonly ILoggerManager _logger;
        private readonly IOptions<AuthSettings> _auth;
        private readonly IHttpClientFactory _httpClient;

        public BankService(IOptions<AuthSettings> auth, IHttpClientFactory httpClient)
        {
            _auth = auth ?? throw new ArgumentNullException(nameof(auth));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<BankResponse> GetAsync()
        {
            var client = _httpClient.CreateClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _auth.Value.BaseUrl)
            {
                Headers = { { HeaderNames.Accept, "application/json" }, { "Ocp-Apim-Subscription-Key", _auth.Value.SubscriptionKey } }
            };
            HttpResponseMessage response = await client.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                BankResponse bankResponse = await response.Content.ReadFromJsonAsync<BankResponse>();
                return bankResponse;
            }
            return null;
        }
    }
}
