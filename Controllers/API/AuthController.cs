using FluentValidation;
using HKDataServices.Service;
using HKDataServices.Model.DTOs;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UsersLoginDto dto)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

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

            return Ok(new
            {
                message = "Login successful.",
                token = auth.Token,
                expiresAt = auth.ExpiresAt,
                user = auth.User
            });
        }

        [HttpGet("validate")]
        [Authorize]
        public IActionResult ValidateToken()
        {
            var header = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(header) || !header.StartsWith("Bearer "))
                return Unauthorized();

            var token = header.Substring("Bearer ".Length).Trim();
            var valid = _authService.ValidateToken(token);

            return valid
                ? Ok(new { message = "Token is valid." })
                : Unauthorized(new { message = "Token is invalid or expired." });
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            if (string.IsNullOrWhiteSpace(dto.UserName) ||
                string.IsNullOrWhiteSpace(dto.CurrentPassword) ||
                string.IsNullOrWhiteSpace(dto.NewPassword))
            {
                return BadRequest("All fields are required.");
            }

            var result = await _authService.ChangePasswordAsync(dto);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = "Password changed successfully." });
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.UserName))
                return BadRequest("Username (email or phone) is required.");

            var result = await _authService.GenerateOtpAsync(dto.UserName);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        [HttpPost("verify-otp")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDto dto)
        {
            if (dto == null ||
                string.IsNullOrWhiteSpace(dto.UserName) ||
                string.IsNullOrWhiteSpace(dto.OtpCode) ||
                string.IsNullOrWhiteSpace(dto.NewPassword))
            {
                return BadRequest("Username, OTP, and NewPassword are required.");
            }

            var result = await _authService.VerifyOtpAndChangePasswordAsync(dto);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }
    }
}