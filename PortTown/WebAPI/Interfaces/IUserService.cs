using Domain;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Interfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(User user, string password);
        Task<JSONFormatter> CheckAvailability(string email);
        Task<User> CreateUserWithTown(User user, string townName);
        Task Logout(string token);
    }
}
