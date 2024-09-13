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
        private readonly IPolicyRepository _policyRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PolicyController(RegisterPolicyUseCase registerPolicyUseCase, GetPolicyByIdUseCase getPolicyByIdUseCase, IHttpContextAccessor httpContextAccessor, IPolicyRepository policyRepository)
        {
            _registerPolicyUseCase = registerPolicyUseCase;
            _getPolicyByIdUseCase = getPolicyByIdUseCase;
            _policyRepository = policyRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePolicy([FromBody] CreatePolicyDto createPolicyDto)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("UserId");
            if (userIdClaim == null)
            {
                return Unauthorized("User not logged in");
            }

            var userId = Guid.Parse(userIdClaim.Value);

            var policyId = await _registerPolicyUseCase.Execute(createPolicyDto, userId);
            return Ok(new { PolicyId = policyId, Message = "Policy created successfully." });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicyById(Guid id)
        {
            var policyDto = await _getPolicyByIdUseCase.ExecuteAsync(id);
            if (policyDto == null)
                return NotFound();

            return Ok(policyDto);
        }
    }
}
