using EL;
using DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL
{
    public class EmpleadoService
    {
        private readonly GenericRepository<Empleado> _repository;

        public EmpleadoService(GenericRepository<Empleado> repository)
        {
            _repository = repository;
        }

        public Task<List<Empleado>> ObtenerTodos() => _repository.GetAllAsync();
        public Task<Empleado?> ObtenerPorId(int id) => _repository.GetByIdAsync(id);
        public Task Agregar(Empleado empleado) => _repository.AddAsync(empleado);
        public Task Actualizar(Empleado empleado) => _repository.UpdateAsync(empleado);
        public Task Eliminar(Empleado empleado) => _repository.DeleteAsync(empleado);
    }
}



