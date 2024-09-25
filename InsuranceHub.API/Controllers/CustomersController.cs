using InsuranceHub.Application.Commands;
using InsuranceHub.Application.DTOS;
using InsuranceHub.Application.Handlers;
using InsuranceHub.Application.RepositoryInterfaces;
using InsuranceHub.Application.ServiceInterfaces;
using InsuranceHub.Application.UseCases;
using InsuranceHub.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
 
        private readonly GetCustomerByIdUseCase _getCustomerByIdUseCase;
        private readonly GetCustomerByUsernameUseCase _getCustomerByUsernameUseCase;
        private readonly CustomerLoginUseCase _customerLoginUseCase;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public CustomersController( GetCustomerByIdUseCase getCustomerByIdUseCase, IEmailService emailservice,ITokenService tokenService,
            GetCustomerByUsernameUseCase getCustomerByUsernameUseCase,CustomerLoginUseCase customerLoginUseCase,
             ICustomerRepository customerRepository, IPasswordHasher passwordHasher)
        {
            
            _getCustomerByIdUseCase = getCustomerByIdUseCase;
            _getCustomerByUsernameUseCase = getCustomerByUsernameUseCase;
            _customerLoginUseCase = customerLoginUseCase;
            _customerRepository = customerRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _emailService = emailservice;
        }

        [HttpPost("Register Customer")]
        public async Task<IActionResult> Register([FromBody] RegisterCustomerCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var handler = new RegisterCustomerCommandHandler(_customerRepository, _passwordHasher);
                await handler.Handle(command);
                return Ok(new { Message = "Customer registered successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
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
            catch (InvalidCredentialsException ex)
            {
                return Unauthorized(new { Message = "Invalid username or password." });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { Message = "Customer not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred." });
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
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO command)
        {
            try
            {
                var useCase = new ForgotPasswordUseCase(_customerRepository, _tokenService, _emailService);
                await useCase.Execute(command);
                return Ok(new { Message = "Password reset token sent to your email." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            try
            {
                var useCase = new ResetPasswordUseCase(_customerRepository, _tokenService, _passwordHasher);
                await useCase.Execute(command);
                return Ok(new { Message = "Password has been reset successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
