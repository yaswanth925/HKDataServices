using HKDataServices.Model;

namespace HKDataServices.Repository
{
    public interface IPreSalesActivityRepository
    {
        Task<IEnumerable<PreSalesActivity>> GetAllAsync(CancellationToken ct);
        Task<PreSalesActivity?> GetByIdAsync(Guid id, CancellationToken ct);
        Task CreateAsync(PreSalesActivity entity, CancellationToken ct);
        void Update(PreSalesActivity entity);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
