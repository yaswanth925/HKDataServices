using HKDataServices.Model.DTOs;
using HKDataServices.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HKDataServices.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PreSalesTargetController : ControllerBase
    {
        private readonly IPreSalesTargetService _service;

        public PreSalesTargetController(IPreSalesTargetService service)
        {
            _service = service;
        }

        [HttpGet("{employeeName}")]
        public async Task<IActionResult> GetByEmployeeName(string employeeName)
        {
            var data = await _service.GetByEmployeeNameAsync(employeeName);
            if (data == null || !data.Any())
                return NotFound(new { message = "No records found for this employee." });

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PreSalesTargetDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            await _service.AddAsync(dto);
            return Ok(new { message = "Pre Sales Target created successfully." });
        }
    }
}
