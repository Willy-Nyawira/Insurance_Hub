using Microsoft.Extensions.Configuration;
using InsuranceHub.Application.ServiceInterfaces;

namespace InsuranceHub.Application.Services
{
    public class OAuthTokenService : IOAuthTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public OAuthTokenService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var response = await _httpClient.GetAsync(_configuration["Mpesa:OAuthBaseUrl"] + "/oauth/v1/generate?grant_type=client_credentials");
            response.EnsureSuccessStatusCode();

            var tokenResponse = await response.Content.ReadAsStringAsync();
            // Parse the JSON response and extract the access token
            // Implement proper parsing logic here
            throw new NotImplementedException("OAuth token parsing not implemented yet.");
        }

    }
}
