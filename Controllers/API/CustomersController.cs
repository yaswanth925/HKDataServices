
using HKDataServices.Model.DTOs;
using HKDataServices.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKDataServices.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _service;

        public CustomersController(ICustomersService service)
        {
            _service = service;
        }

        // 🔹 POST: api/Customers
        [HttpPost]
        [ProducesResponseType(typeof(CustomersResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromForm] CustomersDto form, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            await _service.CreateAsync(form, ct);

            var response = new
            {
                form.CustomerName,
                form.MobileNumber,
                form.EmailId,
                form.GSTNumber,
                form.Address,
                form.Pincode,
                form.City,
                form.State,
                form.Description,
                form.CreatedBy,
            };

            return StatusCode(StatusCodes.Status201Created, response);
        }

        
        [HttpGet("by-name/{customerName}")]
        [ProducesResponseType(typeof(List<CustomersResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCustomerName(string customerName)
        {
            var data = await _service.GetByCustomerNameAsync(customerName);

            if (data == null || !data.Any())
                return NotFound(new { message = "No records found for this customer." });

            var response = data.Select(entity => new CustomersResponseDto
            {
                CustomerID = entity.CustomerID,
                CustomerName = entity.CustomerName,
                MobileNumber = entity.MobileNumber,
                EmailId = entity.EmailId,
                GSTNumber = entity.GSTNumber,
                Address = entity.Address,
                Pincode = entity.Pincode,
                City = entity.City,
                State = entity.State,
                Description = entity.Description,
                PhotoBase64 = entity.PhotoUpload != null ? Convert.ToBase64String(entity.PhotoUpload) : null,
                CreatedBy = entity.CreatedBy,
                Created = entity.Created ?? default,
                ModifiedBy = entity.ModifiedBy,
                Modified = entity.Modified ?? default
            }).ToList();

            return Ok(response);
        }

        [HttpGet("by-mobile/{mobileNumber}")]
        [ProducesResponseType(typeof(List<CustomersResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByMobileNumber(string mobileNumber)
        {
            var data = await _service.GetByMobileNumberAsync(mobileNumber);

            if (data == null || !data.Any())
                return NotFound(new { message = "No records found for this mobile number." });

            var response = data.Select(entity => new CustomersResponseDto
            {
                CustomerID = entity.CustomerID,
                CustomerName = entity.CustomerName,
                MobileNumber = entity.MobileNumber,
                EmailId = entity.EmailId,
                GSTNumber = entity.GSTNumber,
                Address = entity.Address,
                Pincode = entity.Pincode,
                City = entity.City,
                State = entity.State,
                Description = entity.Description,
                PhotoBase64 = entity.PhotoUpload != null ? Convert.ToBase64String(entity.PhotoUpload) : null,
                CreatedBy = entity.CreatedBy,
                Created = entity.Created ?? default,
                ModifiedBy = entity.ModifiedBy,
                Modified = entity.Modified ?? default
            }).ToList();

            return Ok(response);
        }

        [HttpGet("by-email/{emailId}")]
        [ProducesResponseType(typeof(List<CustomersResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByEmailId(string emailId)
        {
            var data = await _service.GetByEmailIdAsync(emailId);

            if (data == null || !data.Any())
                return NotFound(new { message = "No records found for this email ID." });

            var response = data.Select(entity => new CustomersResponseDto
            {
                CustomerID = entity.CustomerID,
                CustomerName = entity.CustomerName,
                MobileNumber = entity.MobileNumber,
                EmailId = entity.EmailId,
                GSTNumber = entity.GSTNumber,
                Address = entity.Address,
                Pincode = entity.Pincode,
                City = entity.City,
                State = entity.State,
                Description = entity.Description,
                PhotoBase64 = entity.PhotoUpload != null ? Convert.ToBase64String(entity.PhotoUpload) : null,
                CreatedBy = entity.CreatedBy,
                Created = entity.Created ?? default,
                ModifiedBy = entity.ModifiedBy,
                Modified = entity.Modified ?? default
            }).ToList();

            return Ok(response);
        }
    }
}
