using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using InsuranceHub.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InsuranceHub.Application.Services
{
    public class MpesaPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MpesaPaymentService> _logger;
        private string _accessToken;
        private DateTime _accessTokenExpiration;

        public MpesaPaymentService(HttpClient httpClient, IConfiguration configuration, ILogger<MpesaPaymentService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            SetBasicAuthHeader();
        }

        private void SetBasicAuthHeader()
        {
            var consumerKey = _configuration["Mpesa:ConsumerKey"];
            var consumerSecret = _configuration["Mpesa:ConsumerSecret"];

            if (string.IsNullOrEmpty(consumerKey) || string.IsNullOrEmpty(consumerSecret))
            {
                throw new ArgumentException("Missing required M-Pesa credentials");
            }

            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{consumerKey}:{consumerSecret}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
            _logger.LogInformation($"Set Authorization header with Basic {auth.Substring(0, 5)}...");
        }

        private async Task<string> GetAccessTokenAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("oauth/v1/generate?grant_type=client_credentials");
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("OAuth Token Response: {Response}", jsonResponse);

                var tokenResponse = JsonSerializer.Deserialize<MpesaTokenResponse>(jsonResponse);
                _accessToken = tokenResponse.AccessToken;
                _accessTokenExpiration = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn);

                return _accessToken;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error retrieving OAuth token from M-Pesa");
                throw;
            }
        }

        public async Task<MpesaStkPushResponse> InitiateStkPushAsync(string phoneNumber, decimal amount, int maxRetries = 3)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                await EnsureValidAccessTokenAsync();

                var stkPushRequest = CreateStkPushRequest(phoneNumber, amount);
                var content = new StringContent(JsonSerializer.Serialize(stkPushRequest), Encoding.UTF8, "application/json");

                var fullUrl = $"{_configuration["Mpesa:BaseUrl"]}{_configuration["Mpesa:StkPushUrl"]}";
                _logger.LogInformation($"Sending STK Push request to: {fullUrl}");
                _logger.LogInformation($"Request body: {JsonSerializer.Serialize(stkPushRequest)}");

                var responseMessage = await _httpClient.PostAsync(fullUrl, content);
                responseMessage.EnsureSuccessStatusCode();

                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                _logger.LogInformation("Raw JSON Response: {Response}", jsonResponse);

                var deserializedResponse = JsonSerializer.Deserialize<MpesaStkPushResponse>(jsonResponse);
                if (deserializedResponse == null)
                {
                    throw new JsonException("Failed to deserialize M-Pesa STK Push response");
                }

                _logger.LogInformation("Deserialized Response: {Response}", deserializedResponse);
                stopwatch.Stop();
                _logger.LogInformation($"STK Push request completed in {stopwatch.ElapsedMilliseconds}ms");
                return deserializedResponse;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound && maxRetries > 0)
            {
                await Task.Delay(1000 * (maxRetries + 1)); // Exponential backoff
                _logger.LogWarning($"STK push request failed due to sandbox environment unavailability. Retrying in {1000 * (maxRetries + 1)}ms...");
                return await InitiateStkPushAsync(phoneNumber, amount, --maxRetries);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Failed to parse STK Push response");
                throw;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "STK push request failed with status code {StatusCode}", ex.StatusCode);
                throw;
            }
        }

        private async Task EnsureValidAccessTokenAsync()
        {
            if (string.IsNullOrEmpty(_accessToken) || DateTime.UtcNow >= _accessTokenExpiration)
            {
                _logger.LogWarning("Access token invalid or expired. Regenerating token.");
                _accessToken = await GetAccessTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            }
        }

        private object CreateStkPushRequest(string phoneNumber, decimal amount)
        {
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var password = Convert.ToBase64String(Encoding.UTF8.GetBytes(
                $"{_configuration["Mpesa:ShortCode"]}{_configuration["Mpesa:Passkey"]}{timestamp}"));

            return new
            {
                BusinessShortCode = _configuration["Mpesa:ShortCode"],
                Password = password,
                Timestamp = timestamp,
                TransactionType = "CustomerPayBillOnline",
                Amount = amount,
                PartyA = phoneNumber,
                PartyB = _configuration["Mpesa:ShortCode"],
                PhoneNumber = phoneNumber,
                CallBackURL = $"{_configuration["Application:Domain"]}/api/payments/mpesa/callback",
                AccountReference = "InsurancePayment",
                TransactionDesc = "Payment for policy"
            };
        }
    }
}
