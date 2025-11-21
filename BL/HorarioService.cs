using DAL;
using EL;

namespace BL
{
    public class HorarioService
    {
        private readonly IUnitOfWork _uow;
        public HorarioService(IUnitOfWork uow) => _uow = uow;

        public async Task<List<Horario>> ObtenerTodosAsync() =>
            await _uow.Horarios.GetAllAsync();

        public async Task<List<Horario>> ObtenerActivosAsync() =>
            (await _uow.Horarios.GetAllAsync())
                .Where(h => h.Activo)
                .OrderBy(h => h.HoraEntrada)
                .ToList();

        public async Task<Horario?> ObtenerPorId(int id) =>
            await _uow.Horarios.GetByIdAsync(id);

        public async Task<Horario> Crear(Horario horario)
        {
            horario.FechaCreacion = DateTime.Now;
            horario.CreadoPor = "Sistema";
            await _uow.Horarios.AddAsync(horario);
            await _uow.SaveChangesAsync();
            return horario;
        }

        public async Task<bool> Actualizar(Horario horario)
        {
            var existente = await ObtenerPorId(horario.IdHorario);
            if (existente == null) return false;

            existente.NombreHorario = horario.NombreHorario.Trim();
            existente.HoraEntrada = horario.HoraEntrada;
            existente.HoraSalida = horario.HoraSalida;
            existente.Activo = horario.Activo;
            existente.FechaModificacion = DateTime.Now;
            existente.ModificadoPor = "Sistema";

            _uow.Horarios.Update(existente);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}