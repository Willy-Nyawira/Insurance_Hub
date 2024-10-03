using System.Text.Json;
using System.Text;
using InsuranceHub.Application.ServiceInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using InsuranceHub.Domain.Models;

namespace InsuranceHub.Application.Services
{
    public class MpesaPaymentService : IMpesaPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ILogger<MpesaPaymentService> _logger;

        public MpesaPaymentService(IConfiguration configuration, HttpClient httpClient, ILogger<MpesaPaymentService> logger)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> InitiateStkPushAsync(StkPushRequest request)
        {
            var accessToken = await GetAccessTokenAsync();

            // Set the Authorization header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Serialize the request object to JSON
            var jsonRequest = JsonSerializer.Serialize(request);

            // Create the StringContent with the JSON data, UTF8 encoding, and media type
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // Post the serialized JSON content to the STK push endpoint
            var response = await _httpClient.PostAsync(_configuration["Mpesa:BaseUrl"] + "/mpesa/stkpush/v1/processrequest", content);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetAccessTokenAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_configuration["Mpesa:OAuthBaseUrl"] + "/oauth/v1/generate?grant_type=client_credentials");
                response.EnsureSuccessStatusCode();

                var tokenResponse = await response.Content.ReadAsStringAsync();
                var tokenObject = JsonSerializer.Deserialize<MpesaTokenResponse>(tokenResponse);

                return tokenObject?.AccessToken ?? string.Empty;
            }
            catch (HttpRequestException ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error retrieving OAuth token from M-Pesa");
                throw;
            }
            catch (JsonException ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error parsing OAuth token response from M-Pesa");
                throw;
            }
        }
    }
}
