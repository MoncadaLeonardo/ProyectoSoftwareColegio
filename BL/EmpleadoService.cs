// BL/EmpleadoService.cs
using DAL;
using EL;

namespace BL
{
    public class EmpleadoService
    {
        private readonly IUnitOfWork _uow;

        public EmpleadoService(IUnitOfWork uow) => _uow = uow;

        public async Task<List<Empleado>> ObtenerTodos()
            => await _uow.Empleados.GetAllAsync();

        public async Task<Empleado?> ObtenerPorId(int id)
            => await _uow.Empleados.GetByIdAsync(id);

        
        public async Task<Empleado> Agregar(Empleado empleado, int idHorario, int idRol)
        {
            empleado.FechaCreacion = DateTime.Now;
            empleado.CreadoPor = "Sistema";
            empleado.Activo = true;

            await _uow.Empleados.AddAsync(empleado);
            await _uow.SaveChangesAsync(); 

            
            var empHorario = new EmpleadoHorario
            {
                IdEmpleado = empleado.IdEmpleado,
                IdHorario = idHorario,
                CreadoPor = "Sistema"
            };
            await _uow.EmpleadoHorarios.AddAsync(empHorario);

            
            var empRol = new EmpleadoRol
            {
                IdEmpleado = empleado.IdEmpleado,
                IdRol = idRol
            };
            await _uow.EmpleadoRoles.AddAsync(empRol);

            await _uow.SaveChangesAsync();
            return empleado;
        }

        
        public async Task<bool> Actualizar(Empleado empleado, int idHorario, int idRol)
        {
            var existente = await _uow.Empleados.GetByIdAsync(empleado.IdEmpleado);
            if (existente == null) return false;

            
            existente.Nombre = empleado.Nombre.Trim();
            existente.Apellido = empleado.Apellido.Trim();
            existente.CedulaNum = empleado.CedulaNum.Trim();
            existente.Email = empleado.Email?.Trim();
            existente.Telefono = empleado.Telefono?.Trim();
            existente.IdDepartamento = empleado.IdDepartamento;
            existente.FechaModificacion = DateTime.Now;
            existente.ModificadoPor = "Sistema";

            _uow.Empleados.Update(existente);

            
            var relHorario = (await _uow.EmpleadoHorarios.GetAllAsync())
                .FirstOrDefault(eh => eh.IdEmpleado == empleado.IdEmpleado);

            if (relHorario != null)
            {
                relHorario.IdHorario = idHorario;
                relHorario.FechaModificacion = DateTime.Now;
                relHorario.ModificadoPor = "Sistema";
                _uow.EmpleadoHorarios.Update(relHorario);
            }
            else
            {
                await _uow.EmpleadoHorarios.AddAsync(new EmpleadoHorario
                {
                    IdEmpleado = empleado.IdEmpleado,
                    IdHorario = idHorario,
                    CreadoPor = "Sistema"
                });
            }

           
            var relRol = (await _uow.EmpleadoRoles.GetAllAsync())
                .FirstOrDefault(er => er.IdEmpleado == empleado.IdEmpleado);

            if (relRol != null)
            {
                relRol.IdRol = idRol;
                _uow.EmpleadoRoles.Update(relRol);
            }
            else
            {
                await _uow.EmpleadoRoles.AddAsync(new EmpleadoRol
                {
                    IdEmpleado = empleado.IdEmpleado,
                    IdRol = idRol
                });
            }

            await _uow.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> Eliminar(int id)
        {
            var empleado = await _uow.Empleados.GetByIdAsync(id);
            if (empleado == null) return false;

            
            var horarios = (await _uow.EmpleadoHorarios.GetAllAsync())
                .Where(eh => eh.IdEmpleado == id);
            foreach (var h in horarios) _uow.EmpleadoHorarios.Delete(h);

            var roles = (await _uow.EmpleadoRoles.GetAllAsync())
                .Where(er => er.IdEmpleado == id);
            foreach (var r in roles) _uow.EmpleadoRoles.Delete(r);

            _uow.Empleados.Delete(empleado);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
