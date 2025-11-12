using HKDataServices.Model;
using HKDataServices.Model.DTOs;

namespace HKDataServices.Repository
{
    public interface IAccountsRepository
    {
        Task<IEnumerable<Accounts>> GetAllAsync(CancellationToken ct);
        Task<Accounts?> GetByDealerCodeAsync(int dealerCode);
        Task<Accounts?> GetByDealerNameAsync(string dealerName);
        Task CreateAsync(Accounts entity, CancellationToken ct);
        Task UpdateAsync(Accounts entity, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
