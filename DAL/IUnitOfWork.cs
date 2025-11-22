// DAL/IUnitOfWork.cs
using EL;

namespace DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Empleado> Empleados { get; }
        IGenericRepository<Departamento> Departamentos { get; }
        IGenericRepository<Rol> Roles { get; }
        IGenericRepository<Horario> Horarios { get; }
        IGenericRepository<Aula> Aulas { get; }
        IGenericRepository<Seccion> Secciones { get; }
        IGenericRepository<EstadoSolicitud> EstadoSolicitudes { get; }
        IGenericRepository<Vacacion> Vacaciones { get; }
        IGenericRepository<SolicitudVacacion> SolicitudVacaciones { get; }
        IGenericRepository<AsignacionProfesor> AsignacionProfesores { get; }
        IGenericRepository<Usuario> Usuarios { get; }
        IGenericRepository<EmpleadoHorario> EmpleadoHorarios { get; }

        IGenericRepository<EmpleadoRol> EmpleadoRoles { get; }

        Task<int> SaveChangesAsync();
    }
}