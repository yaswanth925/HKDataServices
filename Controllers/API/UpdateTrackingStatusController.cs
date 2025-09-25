using HKDataServices.Model.DTOs;
using HKDataServices.Service;
using Microsoft.AspNetCore.Mvc;

namespace HKDataServices.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class UpdateTrackingStatusController(IUpdateTrackingStatusService service) : ControllerBase
{
    private readonly IUpdateTrackingStatusService _service = service;
    [HttpGet]
    [ProducesResponseType(typeof(List<UpdateTrackingStatusResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var entities = await _service.GetAllAsync(ct);

        var response = entities.Select(entity => new UpdateTrackingStatusResponseDto
        {
            ID = entity.ID,
            AWBNumber = entity.AWBNumber,
            StatusType = entity.StatusType,
            FileName = entity.FileName,
            FileData = entity.FileData,
            Remarks = entity.Remarks,
            Createdby = entity.Createdby,
            Created = entity.Created,
            Modifiedby = entity.Modifiedby,
            Modified = entity.Modified
        }).ToList();

        return Ok(response);
    }

    [HttpGet("by-awb/{awbNumber}")]
    [ProducesResponseType(typeof(UpdateTrackingStatusResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByAwbNumber([FromRoute] string awbNumber, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(awbNumber))
            return BadRequest("awbNumber is required.");

        var entity = await _service.GetByAwbNumberAsync(awbNumber, ct);
        if (entity is null) return NotFound();

        var response = new UpdateTrackingStatusResponseDto
        {
            ID = entity.ID,
            AWBNumber = entity.AWBNumber,
            StatusType = entity.StatusType,
            FileName = entity.FileName,
            FileData = entity.FileData,
            Remarks = entity.Remarks,
            Createdby = entity.Createdby,
            Created = entity.Created,
            Modifiedby = entity.Modifiedby,
            Modified = entity.Modified
        };

        return Ok(response);
    }


    [HttpGet("by-id/{id:guid}")]
    [ProducesResponseType(typeof(UpdateTrackingStatusResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByID(Guid id, CancellationToken ct)
    {
        var entity = await service.GetByIDAsync(id, ct);
        if (entity is null) return NotFound();

        var response = new UpdateTrackingStatusResponseDto
        {
            ID = entity.ID,
            AWBNumber = entity.AWBNumber,
            StatusType = entity.StatusType,
            FileName = entity.FileName,
            FileData = entity.FileData,
            Remarks = entity.Remarks,
            Createdby = entity.Createdby,
            Created = entity.Created,
            Modifiedby = entity.Modifiedby,
            Modified = entity.Modified
        };

        return Ok(response);
    }

    [HttpGet("date-range")]
    [ProducesResponseType(typeof(List<UpdateTrackingStatusResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, CancellationToken ct)
    {
        if (startDate > endDate)
        {
            return BadRequest("Start date must be before or equal to end date.");
        }

        var entity = await _service.GetByDateRangeAsync(startDate, endDate, ct);
        if (entity is null) return NotFound();
        var response = entity.Select(entity => new UpdateTrackingStatusResponseDto
        {
            ID = entity.ID,
            AWBNumber = entity.AWBNumber,
            StatusType = entity.StatusType,
            FileName = entity.FileName,
            FileData = entity.FileData,
            Remarks = entity.Remarks, 
            Createdby = entity.Createdby,
            Created = entity.Created,
            Modifiedby = entity.Modifiedby,
            Modified = entity.Modified
        }).ToList();

        return Ok(response);
    }
    [HttpPost]
    [ProducesResponseType(typeof(UpdateTrackingStatusResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromForm] UpdateTrackingStatusFormDto form, CancellationToken ct)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var created = await _service.CreateAsync(form, ct);

        var response = new
        {
            created.ID,
            created.AWBNumber,
            created.StatusType,
            created.FileName,
            created.Remarks,
            created.Created,
        };
        return StatusCode(StatusCodes.Status201Created, response);
    }
    [HttpPut("{awbNumber}")]
    [ProducesResponseType(typeof(UpdateTrackingStatusResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(string awbNumber, [FromForm] UpdateTrackingStatusUpdateDto dto, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(awbNumber)) return BadRequest("AWB number is required.");
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var updated = await _service.UpdateAsync(awbNumber, dto, ct);
        if (updated is null) return NotFound();

        var response = new UpdateTrackingStatusResponseDto
        {
            ID = updated.ID,
            AWBNumber = updated.AWBNumber,
            StatusType = updated.StatusType,
            FileName = updated.FileName,
            FileData = updated.FileData,
            Remarks = updated.Remarks,
            Createdby = updated.Createdby,
            Created = updated.Created,
            Modifiedby = updated.Modifiedby,
            Modified = updated.Modified
        };

        return Ok(response);
    }

        [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UpdateTrackingStatusResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(Guid id, [FromForm] UpdateTrackingStatusUpdateDto dto, CancellationToken ct)
    {
        if (id == Guid.Empty)
            return BadRequest("ID is required.");
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var updated = await _service.UpdateAsync(id, dto, ct);
        if (updated is null)
            return NotFound();

        var response = new UpdateTrackingStatusResponseDto
        {
            ID = updated.ID,
            AWBNumber = updated.AWBNumber,
            StatusType = updated.StatusType,
            FileName = updated.FileName,
            FileData = updated.FileData,
            Remarks = updated.Remarks,
            Createdby = updated.Createdby,
            Created = updated.Created,
            Modifiedby = updated.Modifiedby,
            Modified = updated.Modified
        };

        return Ok(response);
    }
}


