using HKDataServices.Model.DTOs;
using HKDataServices.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;


namespace HKDataServices.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidator<UsersLoginDto>? _validator;

        public AuthController(IAuthService authService, IValidator<UsersLoginDto>? validator = null)
        {
            _authService = authService;
            _validator = validator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsersLoginDto dto)
        {
            if (dto == null) return BadRequest("Request body is required.");

            if (_validator != null)
            {
                var validation = await _validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            if (string.IsNullOrWhiteSpace(dto.UserName))
                return BadRequest("Please provide EmailID or MobileNumber.");

            if (string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Password is required.");

            var auth = await _authService.AuthenticateAsync(dto.UserName, null, dto.Password);
            if (auth == null)
                return Unauthorized(new { message = "Invalid username or password." });

            return Ok(auth);
        }

        [HttpGet("validate")]
        public IActionResult ValidateToken()
        {
            var header = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(header) || !header.StartsWith("Bearer ")) return Unauthorized();

            var token = header.Substring("Bearer ".Length).Trim();
            var valid = _authService.ValidateToken(token);
            return valid ? Ok(new { message = "Token is valid." }) : Unauthorized(new { message = "Token is invalid or expired." });
        }
    }
}

