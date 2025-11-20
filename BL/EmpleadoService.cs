// BL/EmpleadoService.cs
using DAL;
using EL;

namespace BL
{
    public class EmpleadoService
    {
        private readonly IGenericRepository<Empleado> _repo;

        public EmpleadoService(IGenericRepository<Empleado> repo)
        {
            _repo = repo;
        }

        // READ
        public async Task<List<Empleado>> ObtenerTodos()
            => await _repo.GetAllAsync();

        public async Task<Empleado?> ObtenerPorId(int id)
            => await _repo.GetByIdAsync(id);

        // CREATE - Recibe solo los datos necesarios (nunca la entidad completa)
        public async Task<Empleado> Agregar(string nombre, string apellido, string cedulaNum,
            string? email = null, string? telefono = null, int? idDepartamento = null, int? idHorario = null)
        {
            var nuevo = new Empleado
            {
                Nombre = nombre.Trim(),
                Apellido = apellido.Trim(),
                CedulaNum = cedulaNum.Trim(),
                Email = email?.Trim(),
                Telefono = telefono?.Trim(),
                IdDepartamento = idDepartamento,
                IdHorario = idHorario,
                FechaIngreso = DateTime.Today,
                Activo = true,
                FechaCreacion = DateTime.Now,
                CreadoPor = "Sistema" // después lo sacás del usuario logueado
            };

            await _repo.AddAsync(nuevo);
            return nuevo; // devuelve el empleado con Id generado
        }

        // UPDATE - Solo por ID + campos que cambian
        public async Task<bool> Actualizar(int id, string nombre, string apellido, string cedulaNum,
            string? email = null, string? telefono = null, int? idDepartamento = null, int? idHorario = null)
        {
            var existente = await _repo.GetByIdAsync(id);
            if (existente == null) return false;

            // Solo modificamos lo que viene del formulario
            existente.Nombre = nombre.Trim();
            existente.Apellido = apellido.Trim();
            existente.CedulaNum = cedulaNum.Trim();
            existente.Email = email?.Trim();
            existente.Telefono = telefono?.Trim();
            existente.IdDepartamento = idDepartamento;
            existente.IdHorario = idHorario;
            existente.FechaModificacion = DateTime.Now;
            existente.ModificadoPor = "Sistema";

            await _repo.UpdateAsync(existente);
            return true;
        }

        // DELETE - Ya te funcionaba, pero lo dejamos más limpio
        public async Task<bool> Eliminar(int id)
        {
            var empleado = await _repo.GetByIdAsync(id);
            if (empleado == null) return false;

            await _repo.DeleteAsync(empleado);
            return true;
        }
    }
}

