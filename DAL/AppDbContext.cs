using EL;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet para cada tabla
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Seccion> Secciones { get; set; }
        public DbSet<EstadoSolicitud> EstadoSolicitud { get; set; }
        public DbSet<Aula> Aulas { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<AsignacionProfesor> AsignacionProfesores { get; set; }
        public DbSet<Vacacion> Vacaciones { get; set; }
        public DbSet<SolicitudVacacion> SolicitudVacaciones { get; set; }
        public DbSet<EmpleadoHorario> EmpleadoHorarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tablas
            modelBuilder.Entity<Departamento>().ToTable("Departamentos");
            modelBuilder.Entity<Rol>().ToTable("Roles");
            modelBuilder.Entity<Permiso>().ToTable("Permisos");
            modelBuilder.Entity<Seccion>().ToTable("Secciones");
            modelBuilder.Entity<EstadoSolicitud>().ToTable("EstadoSolicitud");
            modelBuilder.Entity<Aula>().ToTable("Aulas");
            modelBuilder.Entity<Horario>().ToTable("Horarios");
            modelBuilder.Entity<Empleado>().ToTable("Empleados");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<AsignacionProfesor>().ToTable("AsignacionProfesores");
            modelBuilder.Entity<Vacacion>().ToTable("Vacaciones");
            modelBuilder.Entity<SolicitudVacacion>().ToTable("SolicitudVacaciones");
            modelBuilder.Entity<EmpleadoHorario>().ToTable("EmpleadoHorarios");

            // Propiedades Unicode false
            modelBuilder.Entity<Empleado>().Property(e => e.Nombre).IsUnicode(false);
            modelBuilder.Entity<Empleado>().Property(e => e.Apellido).IsUnicode(false);
            modelBuilder.Entity<Empleado>().Property(e => e.CedulaNum).IsUnicode(false);
            modelBuilder.Entity<Empleado>().Property(e => e.Email).IsUnicode(false);
            modelBuilder.Entity<Empleado>().Property(e => e.Telefono).IsUnicode(false);
            modelBuilder.Entity<Empleado>().Property(e => e.TelefonoEmergencia).IsUnicode(false);

            modelBuilder.Entity<Usuario>().Property(u => u.UsuarioNombre).IsUnicode(false);

            // Relaciones
            modelBuilder.Entity<Empleado>()
                .HasOne<Empleado>()
                .WithMany()
                .HasForeignKey(e => e.IdSupervisor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Empleado>()
                .HasOne<Horario>()
                .WithMany()
                .HasForeignKey(e => e.IdHorario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Empleado>()
                .HasOne<Departamento>()
                .WithMany()
                .HasForeignKey(e => e.IdDepartamento)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Empleado>()
                .HasKey(a => a.IdEmpleado);


            modelBuilder.Entity<Usuario>()
                .HasOne<Empleado>()
                .WithMany()
                .HasForeignKey(u => u.IdEmpleado)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>()
                .HasOne<Rol>()
                .WithMany()
                .HasForeignKey(u => u.IdRoles)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>()
                .HasKey(a => a.IdUsuario);

            modelBuilder.Entity<AsignacionProfesor>()
                .HasOne<Empleado>()
                .WithMany()
                .HasForeignKey(a => a.IdEmpleado)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AsignacionProfesor>()
                .HasKey(a => a.IdAsignacion);

            modelBuilder.Entity<AsignacionProfesor>()
                .HasOne<Aula>()
                .WithMany()
                .HasForeignKey(a => a.IdAula)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AsignacionProfesor>()
                .HasOne<Seccion>()
                .WithMany()
                .HasForeignKey(a => a.IdSeccion)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vacacion>()
                .HasOne<Empleado>()
                .WithMany()
                .HasForeignKey(v => v.IdEmpleado)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vacacion>()
                .HasKey(a => a.IdVacacion);


            modelBuilder.Entity<SolicitudVacacion>()
                .HasOne<Empleado>()
                .WithMany()
                .HasForeignKey(s => s.IdEmpleado)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SolicitudVacacion>()
                .HasOne<Empleado>()
                .WithMany()
                .HasForeignKey(s => s.IdSupervisor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SolicitudVacacion>()
                .HasOne<EstadoSolicitud>()
                .WithMany()
                .HasForeignKey(s => s.IdEstadoSolicitud)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<SolicitudVacacion>()
                .HasKey(a => a.IdSolicitud);


            modelBuilder.Entity<EmpleadoHorario>()
                .HasOne<Empleado>()
                .WithMany()
                .HasForeignKey(eh => eh.IdEmpleado)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmpleadoHorario>()
                .HasOne<Horario>()
                .WithMany()
                .HasForeignKey(eh => eh.IdHorario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmpleadoHorario>()
                .HasKey(a => a.IdEmpleadoHorario);
        }
    }
}
