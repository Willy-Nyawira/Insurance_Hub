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

        public PaymentsController(MpesaPaymentService mpesaPaymentService, ILogger<PaymentsController> logger, IInvoiceRepository invoiceRepository)
        {
            _mpesaPaymentService = mpesaPaymentService;
            _logger = logger;
            _invoiceRepository = invoiceRepository;
        }

        [HttpPost("mpesa")]
        public async Task<IActionResult> PayWithMpesa([FromBody] MpesaPaymentRequest request)
        {
            try
            {
                _logger.LogInformation("Received M-Pesa payment request for phone number: {PhoneNumber}", request.PhoneNumber);

                var result = await _mpesaPaymentService.InitiateStkPushAsync(request.PhoneNumber, request.Amount);

                _logger.LogInformation("M-Pesa STK Push initiated successfully. Result: {Result}", JsonSerializer.Serialize(result));

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing M-Pesa payment request");
                _logger.LogError("Phone Number: {PhoneNumber}, Amount: {Amount}", request.PhoneNumber, request.Amount);

                // Log the full exception details
                _logger.LogError(ex, "Full Exception Details");

                return StatusCode(500, "An error occurred while processing your request. Please contact support.");
            }

        }

        [HttpPost("mpesa/callback")]
        public async Task<IActionResult> MpesaCallback([FromBody] MpesaCallbackResponse callback)
        {
            if (callback == null)
            {
                return BadRequest("Invalid callback data received.");
            }

            _logger.LogInformation("Mpesa Callback received: {@Callback}", callback);

            if (callback.Body.StkCallback.ResultCode == 0)
            {
                string checkoutRequestId = callback.Body.StkCallback.CheckoutRequestID;
                string mpesaReceiptNumber = callback.Body.StkCallback.CallbackMetadata.Item
                                            .FirstOrDefault(x => x.Name == "MpesaReceiptNumber")?.Value;
                string phoneNumber = callback.Body.StkCallback.CallbackMetadata.Item
                                            .FirstOrDefault(x => x.Name == "PhoneNumber")?.Value;
                decimal amountPaid = Convert.ToDecimal(callback.Body.StkCallback.CallbackMetadata.Item
                                            .FirstOrDefault(x => x.Name == "Amount")?.Value);

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
                _logger.LogWarning("Mpesa payment failed with result code: {ResultCode}, Description: {Description}",
                    callback.Body.StkCallback.ResultCode, callback.Body.StkCallback.ResultDesc);
            }

            return Ok();
        }
    }
}
