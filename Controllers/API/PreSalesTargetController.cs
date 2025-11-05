using HKDataServices.Model;
using HKDataServices.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKDataServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PreSalesTargetController : ControllerBase
    {
        private readonly IPreSalesTargetService _service;

        public PreSalesTargetController(IPreSalesTargetService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{employeeName}")]
        public async Task<IActionResult> GetByEmployeeName(string employeeName)
        {
            var result = await _service.GetByEmployeeNameAsync(employeeName);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PreSalesTarget target)
        {
            await _service.AddAsync(target);
            return Ok(new { message = "PreSalesTarget created successfully." });
        }

        [HttpPut("{employeeName}")]
        public async Task<IActionResult> Update(string employeeName, [FromForm] PreSalesTarget target)
        {
            if (employeeName != target.EmployeeName)
                return BadRequest("EmployeeName mismatch.");

            await _service.UpdateAsync(target);
            return Ok(new { message = "PreSalesTarget updated successfully." });
        }

    }
}