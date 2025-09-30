using HKDataServices.Model.DTOs;
using HKDataServices.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HKDataServices.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _service;
        private object _usersService;

        public UsersController(IUsersService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost]
        [ProducesResponseType(typeof(UsersResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UsersFormDto form, CancellationToken ct)
        {
            var created = await _service.CreateUsersAsync(form, ct);

            var response = new UsersResponseDto
            {
                ID = created.ID,
                FirstName = created.FirstName,
                LastName = created.LastName,
                MobileNumber = created.MobileNumber,
                EmailID = created.EmailID,
                IsActive = created.IsActive,
                Createdby = created.Createdby,
                Created = created.Created
                
            };

            return CreatedAtAction(nameof(Post), new { id = response.ID }, response);
        }
        [HttpGet("by-mobile/{mobileNumber}")]
        [ProducesResponseType(typeof(UsersResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByMobileNumber([FromRoute] string mobileNumber, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(mobileNumber))
                return BadRequest("mobileNumber is required.");

            var entity = await _service.GetByMobileNumberAsync(mobileNumber, ct);
            if (entity is null)
                return NotFound($"User with mobile '{mobileNumber}' not found.");

            var response = new UsersResponseDto
            {
                ID = entity.ID,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                MobileNumber = entity.MobileNumber,
                EmailID = entity.EmailID,
                IsActive = entity.IsActive,
                Createdby = entity.Createdby,
                Created = entity.Created,
                Modifiedby = entity.Modifiedby,
                Modified = entity.Modified
            };

            return Ok(response);
        }

        [HttpGet("by-email/{emailID}")]
        [ProducesResponseType(typeof(UsersResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByEmailID([FromRoute] string emailID, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(emailID))
                return BadRequest("emailID is required.");

            var entity = await _service.GetByEmailIDAsync(emailID, ct);
            if (entity is null)
                return NotFound($"User with email '{emailID}' not found.");

            var response = new UsersResponseDto
            {
                ID = entity.ID,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                MobileNumber = entity.MobileNumber,
                EmailID = entity.EmailID,
                IsActive = entity.IsActive,
                Createdby = entity.Createdby,
                Created = entity.Created,
                Modifiedby = entity.Modifiedby,
                Modified = entity.Modified
            };

            return Ok(response);
        }

        

    }
}
