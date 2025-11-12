using HKDataServices.Model.DTOs;
using HKDataServices.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKDataServices.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _service;

        public AccountsController(IAccountsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _service.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpGet("code/{dealerCode}")]
        public async Task<IActionResult> GetByDealerCode(int dealerCode)
        {
            var result = await _service.GetByDealerCodeAsync(dealerCode);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("name/{dealerName}")]
        public async Task<IActionResult> GetByDealerName(string dealerName)
        {
            var result = await _service.GetByDealerNameAsync(dealerName);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AccountsDto dto, CancellationToken ct)
        {
            var result = await _service.CreateAsync(dto, ct);
            return Ok(result);
        }

        [HttpPut("{dealerCode}")]
        public async Task<IActionResult> Update(int dealerCode, [FromForm] AccountsDto dto, CancellationToken ct)
        {
            var success = await _service.UpdateAsync(dealerCode, dto, ct);
            if (!success) return NotFound();

            return Ok(new { message = "Account updated successfully." });
        }
    }
}
