using HKDataServices.Model.DTOs;
using HKDataServices.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKDataServices.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PreSalesTargetListController : ControllerBase
    {
        private readonly IPreSalesTargetListService _service;

        public PreSalesTargetListController(IPreSalesTargetListService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PreSalesTargetListDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync(CancellationToken ct)
        {
            var result = await _service.GetAllAsync(ct);
            return Ok(result);
        }
    }
}
