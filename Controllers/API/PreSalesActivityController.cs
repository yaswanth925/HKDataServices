using HKDataServices.Model.DTOs;
using HKDataServices.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKDataServices.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PreSalesActivityController : ControllerBase
    {
        private readonly IPreSalesActivityService _service;
        private readonly ICustomersService _customerService;
        private readonly ApplicationDbContext _context; 

        public PreSalesActivityController(
            IPreSalesActivityService service,
            ICustomersService customerService,
            ApplicationDbContext context) 
        {
            _service = service;
            _customerService = customerService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken ct)
        {
            var result = await _service.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var result = await _service.GetByIdAsync(id, ct);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PreSalesActivityDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromForm] PreSalesActivityDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CreateAsync(dto, ct);
            return Ok(result);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] PreSalesActivityDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto, ct);
            if (!updated)
                return NotFound();

            return NoContent();

        }
    }
}
