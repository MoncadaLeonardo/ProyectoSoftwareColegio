using EL;
using System.Security.Claims;

namespace BL.Services.Auth
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(int idEmpleado, string userName, string password, int idRol);
        Task<Usuario?> ValidateUserAsync(string userName, string password);
        Task<ClaimsPrincipal> CreateUserPrincipalAsync(Usuario usuario);
    }
}