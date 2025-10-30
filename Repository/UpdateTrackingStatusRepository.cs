using HKDataServices.Controllers.API;
using HKDataServices.Model;
using HKDataServices.Service;
using Microsoft.EntityFrameworkCore;

namespace HKDataServices.Repository;

public class UpdateTrackingStatusRepository(ApplicationDbContext db) : IUpdateTrackingStatusRepository
{
    public async Task<List<UpdateTrackingStatus>> GetAllAsync(CancellationToken ct = default)
    {
        return await db.UpdateTrackingStatuses
            .AsNoTracking()
            .OrderByDescending(x => x.Created ?? DateTime.MinValue)
            .ThenByDescending(x => x.ID)
            .ToListAsync(ct);
    }
    public async Task<UpdateTrackingStatus> InsertAsync(UpdateTrackingStatus entity, CancellationToken ct = default)
    {
        db.Entry(entity).State = EntityState.Added;
        await db.SaveChangesAsync(ct);
        return entity;
    }
    public async Task<UpdateTrackingStatus?> GetByAwbNumberAsync(string awbNumber, CancellationToken ct = default)
    {
        return await db.UpdateTrackingStatuses
            .AsNoTracking()
            .Where(x => x.AWBNumber == awbNumber)
            .OrderByDescending(x => x.Created ?? DateTime.MinValue)
            .ThenByDescending(x => x.ID)
            .FirstOrDefaultAsync(ct);
    }
    public async Task<UpdateTrackingStatus?> GetByIDAsync(Guid id, CancellationToken ct = default)
    {
        if (id == Guid.Empty) return null;

        return await db.UpdateTrackingStatuses
            .AsNoTracking()
            .Where(x => x.ID == id)
            .OrderByDescending(x => x.Created ?? DateTime.MinValue)
            .ThenByDescending(x => x.ID)
            .FirstOrDefaultAsync(ct);
    }
    public async Task<List<UpdateTrackingStatus>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken ct)
    {
        return await db.UpdateTrackingStatuses
                .AsNoTracking()
                .Where(x => x.Created >= startDate && x.Created <= endDate)
                .ToListAsync(ct);
    }
    public async Task<UpdateTrackingStatus?> GetByAwbNumberForUpdateAsync(string awbNumber, CancellationToken ct = default)
    {
        return await db.UpdateTrackingStatuses
            .Where(x => x.AWBNumber == awbNumber)
            .OrderByDescending(x => x.Created ?? DateTime.MinValue)
            .ThenByDescending(x => x.ID)
            .FirstOrDefaultAsync(ct);
    }

    //public Task<UpdateTrackingStatus?> UpdateByIdAsync(Guid id, UpdateTrackingStatusUpdateDto dto, CancellationToken ct = default)
    //{
    //    throw new NotImplementedException();
    //}

    //public Task<UpdateTrackingStatus?> UpdateByAwbNumberAsync(string awbNumber, UpdateTrackingStatusUpdateDto dto, CancellationToken ct = default)
    //{
    //    throw new NotImplementedException();
    //}

    public Task<UpdateTrackingStatus?> GetByIdForUpdateAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

}