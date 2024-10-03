using InsuranceHub.Application.Commands;
using InsuranceHub.Application.Services;
using Microsoft.AspNetCore.Mvc;
using InsuranceHub.Domain.Models;  // Import the correct namespace
using InsuranceHub.Domain.Enums;
using Microsoft.Extensions.Logging;
using InsuranceHub.Application.RepositoryInterfaces;
using System.Text.Json;

namespace InsuranceHub.API.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly MpesaPaymentService _mpesaPaymentService;
        private readonly ILogger<PaymentsController> _logger;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IConfiguration _configuration;

        public PaymentsController(MpesaPaymentService mpesaPaymentService, IConfiguration configuration, ILogger<PaymentsController> logger, IInvoiceRepository invoiceRepository)
        {
            _mpesaPaymentService = mpesaPaymentService;
            _logger = logger;
            _invoiceRepository = invoiceRepository;
            _configuration = configuration;
        }

        [HttpPost("mpesa")]
        public async Task<IActionResult> PayWithMpesa([FromBody] MpesaPaymentRequest request)
        {
            if (request == null)
            {
                _logger.LogWarning("M-Pesa payment request is null.");
                return BadRequest("Invalid payment request.");
            }

            try
            {
                _logger.LogInformation("Received M-Pesa payment request for phone number: {PhoneNumber}", request.PhoneNumber);

                // Create the STK push request object
                var stkPushRequest = new StkPushRequest
                {
                    BusinessShortCode = _configuration["Mpesa:BusinessShortCode"],
                    Password = GeneratePassword(request), // Ensure this method is correctly implemented
                    Timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss"),
                    TransactionType = "CustomerPayBillOnline",
                    Amount = request.Amount.ToString("F2"), // Ensure proper formatting of the amount
                    PartyA = request.PhoneNumber,
                    PartyB = _configuration["Mpesa:PartyB"],
                    CallBackURL = _configuration["Mpesa:CallBackURL"],
                    AccountReference = request.AccountReference, // Ensure this field exists in MpesaPaymentRequest
                    TransactionDesc = request.TransactionDesc // Ensure this field exists in MpesaPaymentRequest
                };

                var result = await _mpesaPaymentService.InitiateStkPushAsync(stkPushRequest);

                _logger.LogInformation("M-Pesa STK Push initiated successfully. Result: {Result}", JsonSerializer.Serialize(result));

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing M-Pesa payment request");
                return StatusCode(500, "An error occurred while processing your request. Please contact support.");
            }
        }

        [HttpPost("mpesa/callback")]
        public async Task<IActionResult> MpesaCallback([FromBody] MpesaCallbackResponse callback)
        {
            if (callback == null)
            {
                _logger.LogWarning("M-Pesa callback is null.");
                return BadRequest("Invalid callback data received.");
            }

            _logger.LogInformation("M-Pesa Callback received: {@Callback}", callback);

            if (callback.Body.StkCallback.ResultCode == 0)
            {
                string checkoutRequestId = callback.Body.StkCallback.CheckoutRequestID;
                string mpesaReceiptNumber = callback.Body.StkCallback.CallbackMetadata.Item
                                            .FirstOrDefault(x => x.Name == "MpesaReceiptNumber")?.Value;
                string phoneNumber = callback.Body.StkCallback.CallbackMetadata.Item
                                            .FirstOrDefault(x => x.Name == "PhoneNumber")?.Value;
                decimal amountPaid;

                // Try to get the amount paid, ensuring proper handling if the value is null
                var amountValue = callback.Body.StkCallback.CallbackMetadata.Item
                                    .FirstOrDefault(x => x.Name == "Amount")?.Value;

                if (amountValue != null && decimal.TryParse(amountValue, out amountPaid))
                {
                    var invoice = await _invoiceRepository.GetInvoiceByCheckoutRequestIdAsync(checkoutRequestId);
                    if (invoice != null)
                    {
                        invoice.PaymentStatus = PaymentStatus.Paid;
                        invoice.MpesaReceiptNumber = mpesaReceiptNumber;
                        invoice.AmountPaid = amountPaid;
                        invoice.PaymentDate = DateTime.UtcNow;

                        await _invoiceRepository.UpdateInvoiceAsync(invoice);

                        _logger.LogInformation("Invoice {InvoiceId} marked as paid.", invoice.Id);
                    }
                    else
                    {
                        _logger.LogWarning("Invoice not found for CheckoutRequestID: {CheckoutRequestId}", checkoutRequestId);
                    }
                }
                else
                {
                    _logger.LogWarning("Invalid amount received in callback: {AmountValue}", amountValue);
                }
            }
            else
            {
                _logger.LogWarning("M-Pesa payment failed with result code: {ResultCode}, Description: {Description}",
                    callback.Body.StkCallback.ResultCode, callback.Body.StkCallback.ResultDesc);
            }

            return Ok();
        }

        private string GeneratePassword(MpesaPaymentRequest request)
        {
            var businessShortCode = _configuration["Mpesa:BusinessShortCode"];
            var lipaNaMpesaPassword = _configuration["Mpesa:LipaNaMpesaPassword"];
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

            // Combine the values to create the password
            var password = Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes($"{businessShortCode}{lipaNaMpesaPassword}{timestamp}"));

            return password;
        }



    }
}