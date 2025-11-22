// BL/RolService.cs
using DAL;
using EL;

namespace BL
{
    public class RolService
    {
        private readonly IUnitOfWork _uow;

        public RolService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        
        public async Task<List<Rol>> ObtenerTodosAsync()
            => await _uow.Roles.GetAllAsync();

        
        public async Task<List<Rol>> ObtenerActivosAsync()
            => (await _uow.Roles.GetAllAsync())
                .Where(r => r.Activo)
                .OrderBy(r => r.NombreRol)
                .ToList();

        
        public async Task<Rol?> ObtenerPorId(int id)
            => await _uow.Roles.GetByIdAsync(id);

        
        public async Task<Rol> Crear(Rol rol)
        {
            rol.FechaCreacion = DateTime.Now;
            rol.CreadoPor = "Sistema"; 

            await _uow.Roles.AddAsync(rol);
            await _uow.SaveChangesAsync();
            return rol;
        }

        // Actualizar rol
        public async Task<bool> Actualizar(Rol rol)
        {
            var existente = await _uow.Roles.GetByIdAsync(rol.IdRol);
            if (existente == null) return false;

            existente.NombreRol = rol.NombreRol.Trim();
            existente.Activo = rol.Activo;
            existente.FechaModificacion = DateTime.Now;
            existente.ModificadoPor = "Sistema";

            _uow.Roles.Update(existente);
            await _uow.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Desactivar(int id)
        {
            var rol = await _uow.Roles.GetByIdAsync(id);
            if (rol == null) return false;

            rol.Activo = false;
            rol.FechaModificacion = DateTime.Now;
            rol.ModificadoPor = "Sistema";

            _uow.Roles.Update(rol);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}