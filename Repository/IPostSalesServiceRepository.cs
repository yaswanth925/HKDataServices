using HKDataServices.Model;

namespace HKDataServices.Repository
{
    public interface IPostSalesServiceRepository
    {
        Task<IEnumerable<PostSalesService>> GetAllAsync(CancellationToken ct);
        Task<PostSalesService?> GetByIdAsync(Guid id, CancellationToken ct);
        Task CreateAsync(PostSalesService entity, CancellationToken ct);
        void Update(PostSalesService entity);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
