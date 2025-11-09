using HKDataServices.Model;
using HKDataServices.Model.DTOs;

namespace HKDataServices.Service
{
    public interface IAccountsService
    {
        Task<IEnumerable<AccountsDto>> GetAllAsync(CancellationToken ct);
        Task<AccountsDto?> GetByDealerCodeAsync(int dealerCode);
        Task<AccountsDto?> GetByDealerNameAsync(string dealerName);
        Task<AccountsDto> CreateAsync(AccountsDto dto, CancellationToken ct);
        Task<bool> UpdateAsync(int dealerCode, AccountsDto dto, CancellationToken ct);
    }
}
