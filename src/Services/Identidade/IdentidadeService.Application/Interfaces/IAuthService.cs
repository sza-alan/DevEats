using IdentidadeService.Application.DTOs;

namespace IdentidadeService.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterUserDto registerUserDto);
        Task<string?> LoginAsync(LoginUserDto loginUserDto);
    }
}
