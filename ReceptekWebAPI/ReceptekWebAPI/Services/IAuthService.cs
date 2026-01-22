using ReceptekWebAPI.Entities;
using ReceptekWebAPI.Models;

namespace ReceptekWebAPI.Services
{
    public interface IAuthService 
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<string?> LoginAsync(UserDto request);
    }
}
