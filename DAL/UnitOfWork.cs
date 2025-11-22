// DAL/UnitOfWork.cs
using EL;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context; // ← CORREGIDO

        // Repositorios
        public IGenericRepository<Empleado> Empleados { get; private set; }
        public IGenericRepository<Departamento> Departamentos { get; private set; }
        public IGenericRepository<Rol> Roles { get; private set; }
        public IGenericRepository<Horario> Horarios { get; private set; }
        public IGenericRepository<Aula> Aulas { get; private set; }
        public IGenericRepository<Seccion> Secciones { get; private set; }
        public IGenericRepository<EstadoSolicitud> EstadoSolicitudes { get; private set; }
        public IGenericRepository<Vacacion> Vacaciones { get; private set; }
        public IGenericRepository<SolicitudVacacion> SolicitudVacaciones { get; private set; }
        public IGenericRepository<AsignacionProfesor> AsignacionProfesores { get; private set; }
        public IGenericRepository<Usuario> Usuarios { get; private set; }
        public IGenericRepository<EmpleadoHorario> EmpleadoHorarios { get; private set; }
        public IGenericRepository<EmpleadoRol> EmpleadoRoles { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            // Inicializar todos los repositorios
            Empleados = new GenericRepository<Empleado>(_context);
            Departamentos = new GenericRepository<Departamento>(_context);
            Roles = new GenericRepository<Rol>(_context);
            Horarios = new GenericRepository<Horario>(_context);
            Aulas = new GenericRepository<Aula>(_context);
            Secciones = new GenericRepository<Seccion>(_context);
            EstadoSolicitudes = new GenericRepository<EstadoSolicitud>(_context);
            Vacaciones = new GenericRepository<Vacacion>(_context);
            SolicitudVacaciones = new GenericRepository<SolicitudVacacion>(_context);
            AsignacionProfesores = new GenericRepository<AsignacionProfesor>(_context);
            Usuarios = new GenericRepository<Usuario>(_context);
            EmpleadoHorarios = new GenericRepository<EmpleadoHorario>(_context);
            EmpleadoRoles = new GenericRepository<EmpleadoRol>(_context); // ← AHORA SÍ
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}