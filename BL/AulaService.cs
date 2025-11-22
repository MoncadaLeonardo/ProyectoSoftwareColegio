// BL/AulaService.cs
using DAL;
using EL;

namespace BL
{
    public class AulaService
    {
        private readonly IUnitOfWork _uow;
        public AulaService(IUnitOfWork uow) => _uow = uow;

        public async Task<List<Aula>> ObtenerTodosAsync() =>
            await _uow.Aulas.GetAllAsync();

        public async Task<List<Aula>> ObtenerActivosAsync() =>
            (await _uow.Aulas.GetAllAsync())
                .Where(a => a.Activo)
                .OrderBy(a => a.NombreAula)
                .ToList();

        public async Task<Aula?> ObtenerPorId(int id) =>
            await _uow.Aulas.GetByIdAsync(id);

        public async Task<Aula> Crear(Aula aula)
        {
            aula.FechaCreacion = DateTime.Now;
            aula.CreadoPor = "Sistema";
            await _uow.Aulas.AddAsync(aula);
            await _uow.SaveChangesAsync();
            return aula;
        }

        public async Task<bool> Actualizar(Aula aula)
        {
            var existente = await ObtenerPorId(aula.IdAula);
            if (existente == null) return false;

            existente.NombreAula = aula.NombreAula.Trim();
            existente.Activo = aula.Activo;
            existente.FechaModificacion = DateTime.Now;
            existente.ModificadoPor = "Sistema";

            _uow.Aulas.Update(existente);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
