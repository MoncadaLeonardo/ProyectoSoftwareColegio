using DAL;
using EL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text; // Necesario para Encoding

namespace BL.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> RegisterAsync(int idEmpleado, string userName, string password, int idRol)
        {
            var todos = await _unitOfWork.Usuarios.GetAllAsync();
            // Verifica si el usuario ya existe
            if (todos.Any(u => u.UserName == userName)) return false;

            var nuevo = new Usuario
            {
                IdEmpleado = idEmpleado,
                UserName = userName,
                // Aquí usamos el nuevo método de hash determinista
                PasswordHash = HashPassword(password),
                IdRoles = idRol,
                Activo = true
            };

            await _unitOfWork.Usuarios.AddAsync(nuevo);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario?> ValidateUserAsync(string userName, string password)
        {
            var todos = await _unitOfWork.Usuarios.GetAllAsync();
            // Busca usuario activo
            var usuario = todos.FirstOrDefault(u => u.UserName == userName && u.Activo);

            if (usuario == null) return null;

            // Compara la contraseña ingresada con la guardada
            return VerifyPasswordHash(password, usuario.PasswordHash) ? usuario : null;
        }

        public Task<ClaimsPrincipal> CreateUserPrincipalAsync(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.UserName),
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim("IdEmpleado", usuario.IdEmpleado.ToString()),
                new Claim(ClaimTypes.Role, "Usuario") // Aquí puedes poner lógica para roles reales si tienes la tabla Roles cargada
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            return Task.FromResult(principal);
        }

        // ==========================================
        // CORRECCIÓN APLICADA AQUÍ ABAJO
        // ==========================================

        private byte[] HashPassword(string password)
        {
            // Usamos SHA256 simple. Esto genera siempre el mismo hash para el mismo texto.
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash)
        {
            // 1. Hasheamos la contraseña que el usuario acaba de escribir
            var computedHash = HashPassword(password);

            // 2. Comparamos byte por byte con lo que hay en la base de datos
            return computedHash.SequenceEqual(storedHash);
        }
    }
}