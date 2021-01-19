using Domain;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(string email, string password);
        Task<bool> CheckAvailability(string email);
        Task Logout(string token);
    }
}
