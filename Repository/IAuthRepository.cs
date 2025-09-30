using System;
using System.Threading.Tasks;
using HKDataServices.Model;

namespace HKDataServices.Repository
{
    public interface IAuthRepository
    {
        Task<Users?> GetUserByEmailOrMobileAsync(string? email, string? mobile);
        Task<Users> GetByEmailOrMobileAsync(string? email, string? mobile);
        Task UpdatePasswordAsync(Guid iD);
        
    }
}
