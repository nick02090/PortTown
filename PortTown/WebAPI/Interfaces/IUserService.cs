using Domain;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(User user, string password);
        Task<dynamic> CheckAvailability(string email);
        Task<User> CreateUserWithTown(User user, string townName);
        Task Logout(string token);
    }
}
