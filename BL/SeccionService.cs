// BL/SeccionService.cs
using DAL;
using EL;

namespace BL
{
    public class SeccionService
    {
        private readonly IUnitOfWork _uow;
        public SeccionService(IUnitOfWork uow) => _uow = uow;

        public async Task<List<Seccion>> ObtenerTodosAsync() =>
            await _uow.Secciones.GetAllAsync();

        public async Task<List<Seccion>> ObtenerActivosAsync() =>
            (await _uow.Secciones.GetAllAsync())
                .Where(s => s.Activo)
                .OrderBy(s => s.NombreSeccion)
                .ToList();

        public async Task<Seccion?> ObtenerPorId(int id) =>
            await _uow.Secciones.GetByIdAsync(id);

        public async Task<Seccion> Crear(Seccion seccion)
        {
            seccion.FechaCreacion = DateTime.Now;
            seccion.CreadoPor = "Sistema";
            await _uow.Secciones.AddAsync(seccion);
            await _uow.SaveChangesAsync();
            return seccion;
        }

        public async Task<bool> Actualizar(Seccion seccion)
        {
            var existente = await ObtenerPorId(seccion.IdSeccion);
            if (existente == null) return false;

            existente.NombreSeccion = seccion.NombreSeccion.Trim();
            existente.Activo = seccion.Activo;
            existente.FechaModificacion = DateTime.Now;
            existente.ModificadoPor = "Sistema";

            _uow.Secciones.Update(existente);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}