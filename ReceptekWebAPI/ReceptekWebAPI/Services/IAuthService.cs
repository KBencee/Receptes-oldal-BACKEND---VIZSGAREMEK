using ReceptekWebAPI.Entities;
using ReceptekWebAPI.Models;

namespace ReceptekWebAPI.Services
{
    public interface IAuthService 
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request);
    }
}
