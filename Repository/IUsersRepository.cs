using HKDataServices.Model;
using System.Threading;
using System.Threading.Tasks;

namespace HKDataServices.Repository
{
    public interface IUsersRepository
    {
        Task<Users> InsertAsync(Users entity, CancellationToken ct = default);
        Task<Users?> GetByMobileNumberAsync(string mobileNumber, CancellationToken ct = default);
        Task<Users?> GetByEmailIDAsync(string emailID, CancellationToken ct = default);

    }
}