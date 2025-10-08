using HKDataServices.Model;
using HKDataServices.Model.DTOs;
using System.Threading.Tasks;
namespace HKDataServices.Service
{


    public interface IAuthService
    {
        Task<AuthResponseDto?> AuthenticateAsync(string? email, string? mobile, string password);
        Task AuthenticateAsync(string username, string password);
        bool ValidateToken(string token);
    }
}
