// BL/DepartamentoService.cs
using DAL;
using EL;

namespace BL
{
    public class DepartamentoService
    {
        private readonly IUnitOfWork _uow;

        public DepartamentoService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        
        public async Task<List<Departamento>> ObtenerTodosAsync()
            => await _uow.Departamentos.GetAllAsync();

        
        public async Task<List<Departamento>> ObtenerActivosAsync()
            => (await _uow.Departamentos.GetAllAsync())
                .Where(d => d.Activo)
                .OrderBy(d => d.NombreDepartamento)
                .ToList();

        
        public async Task<Departamento?> ObtenerPorId(int id)
            => await _uow.Departamentos.GetByIdAsync(id);

        public async Task<Departamento> Crear(Departamento dept)
        {
            dept.FechaCreacion = DateTime.Now;
            dept.CreadoPor = "Sistema";
            dept.Activo = true;

            await _uow.Departamentos.AddAsync(dept);
            await _uow.SaveChangesAsync();
            return dept;
        }

        
        public async Task<bool> Actualizar(Departamento dept)
        {
            var existente = await ObtenerPorId(dept.IdDepartamento);
            if (existente == null) return false;

            existente.NombreDepartamento = dept.NombreDepartamento.Trim();
            existente.Activo = dept.Activo;
            existente.FechaModificacion = DateTime.Now;
            existente.ModificadoPor = "Sistema"; 

            _uow.Departamentos.Update(existente);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}