using InsuranceHub.Application.DTOS;
using InsuranceHub.Application.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly RegisterCustomerUseCase _registerCustomerUseCase;
        private readonly GetCustomerByIdUseCase _getCustomerByIdUseCase;
        private readonly GetCustomerByUsernameUseCase _getCustomerByUsernameUseCase;
        private readonly CustomerLoginUseCase _customerLoginUseCase;

        public CustomersController(RegisterCustomerUseCase registerCustomerUseCase, GetCustomerByIdUseCase getCustomerByIdUseCase, GetCustomerByUsernameUseCase getCustomerByUsernameUseCase,CustomerLoginUseCase customerLoginUseCase)
        {
            _registerCustomerUseCase = registerCustomerUseCase;
            _getCustomerByIdUseCase = getCustomerByIdUseCase;
            _getCustomerByUsernameUseCase = getCustomerByUsernameUseCase;
            _customerLoginUseCase = customerLoginUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CustomerRegistrationDto dto)
        {
            var customerId = await _registerCustomerUseCase.Execute(dto);
            var message = $"Customer successfully registered with ID: {customerId}";
            return Ok(new { Message = message, CustomerId = customerId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var customer = await _getCustomerByIdUseCase.Execute(id);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                // Handle the exception, maybe return a 404 Not Found or other appropriate status code
                return NotFound(ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _customerLoginUseCase.Execute(loginDto);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }
        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            try
            {
                var customer = await _getCustomerByUsernameUseCase.Execute(username);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
