using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly string _baseUrl;

        public MpesaPaymentService(HttpClient httpClient, IConfiguration configuration, ILogger<MpesaPaymentService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            _baseUrl = _configuration["Mpesa:BaseUrl"];
        }

        // Method to get OAuth token from Mpesa
        public async Task<string> GetOAuthTokenAsync()
        {
            try
            {
                var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(
         $"{_configuration["Mpesa:ConsumerKey"]}:{_configuration["Mpesa:ConsumerSecret"]}"));

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", auth);

                var response = await _httpClient.GetAsync($"{_baseUrl}{_configuration["Mpesa:TokenUrl"]}");
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<MpesaTokenResponse>(jsonResponse);

                return tokenResponse.AccessToken;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error retrieving OAuth token from M-Pesa.");
                throw new Exception("Failed to retrieve OAuth token from M-Pesa.", ex);
            }
        }

        public async Task<MpesaStkPushResponse> InitiateStkPushAsync(string phoneNumber, decimal amount)
        {
            try
            {
                var token = await GetOAuthTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                var password = Convert.ToBase64String(Encoding.UTF8.GetBytes(
                    _configuration["Mpesa:ShortCode"] + _configuration["Mpesa:Passkey"] + timestamp));
                var accountReference = "InsurancePayment";

                var stkPushRequest = new
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
                    AccountReference = accountReference,
                    TransactionDesc = "Payment for policy"
                };

                var content = new StringContent(JsonSerializer.Serialize(stkPushRequest), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}{_configuration["Mpesa:StkPushUrl"]}", content);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonResponse);
                var stkPushResponse = JsonSerializer.Deserialize<MpesaStkPushResponse>(jsonResponse);

                return stkPushResponse;
            }
            catch (HttpRequestException ex)
            {
                var errorMessage = $"STK push request failed with status code {ex.StatusCode}.";

                throw new Exception(errorMessage, ex);
            }
        }

    }
}

