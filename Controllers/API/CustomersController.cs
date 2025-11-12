using HKDataServices.Model.DTOs;
using HKDataServices.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HKDataServices.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _service;
        private readonly ApplicationDbContext _context;
        public CustomersController(ICustomersService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CustomersDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CustomersDto>>> GetAllAsync(CancellationToken ct)
        {
            var customers = await _service.GetAllAsync(ct);
            if (customers == null || !customers.Any()) return NotFound("No customers found.");
            return Ok(customers);
        }

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
                ImageBase64 = entity.ImageFile != null ? Convert.ToBase64String(entity.ImageFile) : null,
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
                ImageBase64 = entity.ImageFile != null ? Convert.ToBase64String(entity.ImageFile) : null,
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
                ImageBase64 = entity.ImageFile != null ? Convert.ToBase64String(entity.ImageFile) : null,
                CreatedBy = entity.CreatedBy,
                Created = entity.Created ?? default,
                ModifiedBy = entity.ModifiedBy,
                Modified = entity.Modified ?? default
            }).ToList();

            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromForm] CustomersDto dto, CancellationToken ct)

        {
            if (dto == null || dto.CustomerID == Guid.Empty)
                return BadRequest("Invalid customer data.");

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerID == dto.CustomerID, ct);

            if (customer == null)
                return NotFound($"Customer with ID {dto.CustomerID} not found.");
            customer.CustomerName = dto.CustomerName;
            customer.MobileNumber = dto.MobileNumber;
            customer.EmailId = dto.EmailId;
            customer.GSTNumber = dto.GSTNumber;
            customer.Address = dto.Address;
            customer.Pincode = dto.Pincode;
            customer.City = dto.City;
            customer.State = dto.State;
            customer.Description = dto.Description;
            customer.ModifiedBy = dto.ModifiedBy;
            customer.Modified = DateTime.UtcNow;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync(ct);

            return Ok("Customer updated successfully.");
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> PatchAsync(Guid id, [FromForm] CustomersPatchDto dto, CancellationToken ct)
        {
            if (dto == null)
                return BadRequest("Invalid customer data.");

            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerID == id, ct);

            if (customer == null)
                return NotFound($"Customer with ID {id} not found.");

            if (!string.IsNullOrWhiteSpace(dto.CustomerName))
                customer.CustomerName = dto.CustomerName;

            if (!string.IsNullOrWhiteSpace(dto.MobileNumber))
                customer.MobileNumber = dto.MobileNumber;

            if (!string.IsNullOrWhiteSpace(dto.EmailId))
                customer.EmailId = dto.EmailId;

            if (!string.IsNullOrWhiteSpace(dto.Address))
                customer.Address = dto.Address;

            if (!string.IsNullOrWhiteSpace(dto.City))
                customer.City = dto.City;

            if (!string.IsNullOrWhiteSpace(dto.Pincode))
                customer.Pincode = dto.Pincode;

            if (!string.IsNullOrWhiteSpace(dto.State))
                customer.State = dto.State;

            customer.Modified = DateTime.UtcNow;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync(ct);

            return Ok("Customer details updated successfully.");
        }
    }
}
