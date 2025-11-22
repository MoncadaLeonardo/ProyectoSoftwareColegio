// DAL/ApplicationDbContext.cs
using EL;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       
        public DbSet<Empleado> Empleados { get; set; } = null!;
        public DbSet<Departamento> Departamentos { get; set; } = null!;
        public DbSet<Rol> Roles { get; set; } = null!;
        public DbSet<Horario> Horarios { get; set; } = null!;
        public DbSet<Aula> Aulas { get; set; } = null!;
        public DbSet<Seccion> Secciones { get; set; } = null!;
        public DbSet<EstadoSolicitud> EstadoSolicitudes { get; set; } = null!;
        public DbSet<Vacacion> Vacaciones { get; set; } = null!;
        public DbSet<SolicitudVacacion> SolicitudVacaciones { get; set; } = null!;
        public DbSet<AsignacionProfesor> AsignacionProfesores { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<EmpleadoHorario> EmpleadoHorarios { get; set; } = null!;
        public DbSet<EmpleadoRol> EmpleadoRoles { get; set; } = null!; 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<EmpleadoRol>(entity =>
            {
                entity.HasKey(er => new { er.IdEmpleado, er.IdRol });

                entity.ToTable("EmpleadoRoles");

                entity.HasOne(er => er.Empleado)
                      .WithMany(e => e.EmpleadoRoles)
                      .HasForeignKey(er => er.IdEmpleado)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(er => er.Rol)
                      .WithMany(r => r.EmpleadoRoles)
                      .HasForeignKey(er => er.IdRol)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(er => er.FechaAsignacion)
                      .HasDefaultValueSql("GETDATE()");
            });

           
            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.ToTable("Empleados");
                entity.HasIndex(e => e.CedulaNum).IsUnique();
            });

            
            modelBuilder.Entity<Horario>(entity =>
            {
                entity.ToTable("Horarios");
                entity.Property(h => h.HoraEntrada).HasColumnType("time");
                entity.Property(h => h.HoraSalida).HasColumnType("time");
            });

            
        }
    }
}