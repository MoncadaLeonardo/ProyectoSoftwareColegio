// BL/EmpleadoService.cs
using DAL;
using EL;

namespace BL
{
    public class EmpleadoService
    {
        private readonly IUnitOfWork _uow;

        public EmpleadoService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<Empleado>> ObtenerTodos()
            => await _uow.Empleados.GetAllAsync();

        public async Task<Empleado?> ObtenerPorId(int id)
            => await _uow.Empleados.GetByIdAsync(id);

        public async Task<Empleado> Agregar(Empleado empleado)
        {
            empleado.FechaCreacion = DateTime.Now;
            empleado.CreadoPor = "Sistema"; // después lo sacas del usuario autenticado
            empleado.Activo = true;

            await _uow.Empleados.AddAsync(empleado);
            await _uow.SaveChangesAsync();
            return empleado;
        }

        public async Task<bool> Actualizar(Empleado empleadoActualizado)
        {
            var existente = await _uow.Empleados.GetByIdAsync(empleadoActualizado.IdEmpleado);
            if (existente == null) return false;

            // Actualizamos solo los campos que cambian
            existente.Nombre = empleadoActualizado.Nombre.Trim();
            existente.Apellido = empleadoActualizado.Apellido.Trim();
            existente.CedulaNum = empleadoActualizado.CedulaNum.Trim();
            existente.Email = empleadoActualizado.Email?.Trim();
            existente.Telefono = empleadoActualizado.Telefono?.Trim();
            existente.IdDepartamento = empleadoActualizado.IdDepartamento;
            existente.IdHorario = empleadoActualizado.IdHorario;

            existente.FechaModificacion = DateTime.Now;
            existente.ModificadoPor = "Sistema";

            _uow.Empleados.Update(existente);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            var empleado = await _uow.Empleados.GetByIdAsync(id);
            if (empleado == null) return false;

            _uow.Empleados.Delete(empleado);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}

