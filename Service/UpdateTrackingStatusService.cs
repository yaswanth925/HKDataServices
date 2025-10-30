using HKDataServices.Model;
using HKDataServices.Model.DTOs;
using HKDataServices.Repository;

namespace HKDataServices.Service;

public class UpdateTrackingStatusService(IUpdateTrackingStatusRepository repo) : IUpdateTrackingStatusService
{

    public async Task<UpdateTrackingStatus> CreateAsync(UpdateTrackingStatusFormDto dto, CancellationToken ct = default)
    {
        if (dto is null) throw new ArgumentNullException(nameof(dto));
        if (dto.FileData is null || dto.FileData.Length == 0)
            throw new ArgumentException("FileData is required and cannot be empty.");


        byte[] bytes;
        using (var ms = new MemoryStream())
        {
            await dto.FileData.CopyToAsync(ms, ct);
            bytes = ms.ToArray();
        }

        var entity = new UpdateTrackingStatus
        {
            ID = Guid.NewGuid(),
            AWBNumber = dto.AWBNumber,
            StatusType = dto.StatusType,
            FileName = string.IsNullOrWhiteSpace(dto.FileName) ? dto.FileData.FileName : dto.FileName,
            FileData = bytes,
            Remarks = dto.Remarks,
            CreatedBy = string.IsNullOrWhiteSpace(dto.CreatedBy) ? "system" : dto.CreatedBy,
            Created = DateTime.UtcNow
        };

        return await repo.InsertAsync(entity, ct);
    }
    public Task<List<UpdateTrackingStatus>> GetAllAsync(CancellationToken ct = default)
    {
        return repo.GetAllAsync(ct);
    }
    public Task<UpdateTrackingStatus?> GetByAwbNumberAsync(string awbNumber, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(awbNumber))
            throw new ArgumentException("AWB number is required.", nameof(awbNumber));

        return repo.GetByAwbNumberAsync(awbNumber, ct);
    }
    public Task<UpdateTrackingStatus?> GetByIDAsync(Guid id, CancellationToken ct = default)
    {
        if (id == Guid.Empty)
            return Task.FromResult<UpdateTrackingStatus?>(null);

        return repo.GetByIDAsync(id, ct);
    }


    public Task<List<UpdateTrackingStatus>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken ct = default)
    {
        if (startDate > endDate)
            throw new ArgumentException("Start date must be before or equal to end date.");

        return repo.GetByDateRangeAsync(startDate, endDate, ct);
    }

    public Task<UpdateTrackingStatus?> UpdateAsync(Guid id, UpdateTrackingStatusUpdateDto dto, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<UpdateTrackingStatus?> UpdateAsync(string awbNumber, UpdateTrackingStatusUpdateDto dto, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
