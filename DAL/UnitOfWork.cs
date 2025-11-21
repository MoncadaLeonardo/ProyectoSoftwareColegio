// DAL/UnitOfWork.cs
using EL;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

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

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Empleados = new GenericRepository<Empleado>(context);
            Departamentos = new GenericRepository<Departamento>(context);
            Roles = new GenericRepository<Rol>(context);
            Horarios = new GenericRepository<Horario>(context);
            Aulas = new GenericRepository<Aula>(context);
            Secciones = new GenericRepository<Seccion>(context);
            EstadoSolicitudes = new GenericRepository<EstadoSolicitud>(context);
            Vacaciones = new GenericRepository<Vacacion>(context);
            SolicitudVacaciones = new GenericRepository<SolicitudVacacion>(context);
            AsignacionProfesores = new GenericRepository<AsignacionProfesor>(context);
            Usuarios = new GenericRepository<Usuario>(context);
            EmpleadoHorarios = new GenericRepository<EmpleadoHorario>(context);
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