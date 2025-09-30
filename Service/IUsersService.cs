using HKDataServices.Model;
using HKDataServices.Model.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace HKDataServices.Service
{
    public interface IUsersService
    {
        Task<UsersResponseDto> CreateUsersAsync(UsersFormDto dto, CancellationToken ct = default);
        Task<Users?> GetByMobileNumberAsync(string MobileNumber, CancellationToken ct = default);
        Task<Users?> GetByEmailIDAsync(string EmailID, CancellationToken ct = default);
    }
}
    