using System.Security.Claims;
using InsuranceHub.Application.DTOS;
using InsuranceHub.Application.RepositoryInterfaces;
using InsuranceHub.Application.UseCases;
using InsuranceHub.Domain.Entities;
using InsuranceHub.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceHub.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly RegisterPolicyUseCase _registerPolicyUseCase;
        private readonly GetPolicyByIdUseCase _getPolicyByIdUseCase;
        private readonly DeletePolicyUseCase _deletePolicyUseCase;
        private readonly UpdatePolicyUseCase _updatePolicyUseCase;
        private readonly GetPoliciesByCustomerUseCase _getPoliciesByCustomerUseCase;
        private readonly GetCustomerByUsernameUseCase _getCustomerByUsernameUseCase;
        private readonly IPolicyRepository _policyRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PurchasePolicyUseCase _purchasePolicyUseCase;

        public PolicyController(RegisterPolicyUseCase registerPolicyUseCase, GetPolicyByIdUseCase getPolicyByIdUseCase,
            IHttpContextAccessor httpContextAccessor, IPolicyRepository policyRepository,
            DeletePolicyUseCase deletePolicyUseCase, UpdatePolicyUseCase updatePolicyUseCase,
            GetPoliciesByCustomerUseCase getPoliciesByCustomerUseCase,
            GetCustomerByUsernameUseCase getCustomerByUsernameUseCase, PurchasePolicyUseCase purchasePolicyUseCase)
        {
            _registerPolicyUseCase = registerPolicyUseCase;
            _getPolicyByIdUseCase = getPolicyByIdUseCase;
            _policyRepository = policyRepository;
            _httpContextAccessor = httpContextAccessor;
            _deletePolicyUseCase = deletePolicyUseCase;
            _updatePolicyUseCase = updatePolicyUseCase;
            _getPoliciesByCustomerUseCase = getPoliciesByCustomerUseCase;
            _getCustomerByUsernameUseCase = getCustomerByUsernameUseCase;
            _purchasePolicyUseCase = purchasePolicyUseCase;
        }
        [Authorize(Roles = "User,Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreatePolicy([FromBody] CreatePolicyDto createPolicyDto)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name) ??
                              _httpContextAccessor.HttpContext.User.FindFirst("unique_name");


            if (userIdClaim == null)
            {
                return Unauthorized("User not logged in");
            }

            var userId = userIdClaim.Value;
            var policyId = await _registerPolicyUseCase.Execute(createPolicyDto, userId);
            return Ok(new { PolicyId = policyId, Message = "Policy created successfully." });
        }
        [Authorize(Roles = "Admin,Customer,User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicyById(Guid id)
        {
            var policyDto = await _getPolicyByIdUseCase.ExecuteAsync(id);
            if (policyDto == null)
                return NotFound();

            return Ok(policyDto);
        }
        [Authorize(Roles = "Admin,User,Customer")]
        [HttpGet("my_policies")]
        public async Task<IActionResult> GetPolicies()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name) ??
                              _httpContextAccessor.HttpContext.User.FindFirst("unique_name");

            if (userIdClaim == null)
            {
                return Unauthorized("User not logged in");
            }

            var username = userIdClaim.Value;

            // Get the customer based on username (you need to implement this use case)
            var customer = await _getCustomerByUsernameUseCase.Execute(username);

            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            // Fetch policies using the GetPoliciesByCustomerUseCase
            var policies = await _getPoliciesByCustomerUseCase.Execute(customer.Id);

            return Ok(policies);
        }
        [Authorize(Roles = "Admin,User")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdatePolicy([FromBody] UpdatePolicyDto updatePolicyDto)
        {
            // First, search for the policy by its ID
            var policy = await _getPolicyByIdUseCase.ExecuteAsync(updatePolicyDto.Id);

            if (policy == null)
            {
                return NotFound(new { Message = "Policy not found." });
            }

            // Proceed with updating the policy
            await _updatePolicyUseCase.ExecuteAsync(updatePolicyDto);
            return Ok(new { Message = "Policy updated successfully." });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicy(Guid id)
        {
            await _deletePolicyUseCase.ExecuteAsync(id);
            return Ok(new { Message = "Policy deleted successfully." });
        }
        [Authorize(Roles = "Admin,User,Customer")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPolicies()
        {
            // Retrieve all policies using a repository or use case
            var policies = await _policyRepository.GetAllPoliciesAsync();

            if (policies == null || !policies.Any())
            {
                return NotFound(new { Message = "No policies found." });
            }

            return Ok(policies);
        }

        [HttpPost("{id}/buy")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> BuyPolicy(Guid id)
        {
            try
            {
                var invoice = await _purchasePolicyUseCase.Execute(id);
                return Ok(new { Message = "Policy purchased successfully", InvoiceId = invoice.Id });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
