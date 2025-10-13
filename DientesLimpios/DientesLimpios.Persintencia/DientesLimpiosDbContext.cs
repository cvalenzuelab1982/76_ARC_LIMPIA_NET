using DientesLimpios.Aplicacion.Contratos.Identidad;
using DientesLimpios.Dominio.Comunes;
using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace DientesLimpios.Persintencia
{
    public class DientesLimpiosDbContext : DbContext
    {
        private readonly IServiciosUsuarios? _serviciosUsuarios;

        public DientesLimpiosDbContext(DbContextOptions<DientesLimpiosDbContext> options, IServiciosUsuarios serviciosUsuarios) : base(options)
        {
            _serviciosUsuarios = serviciosUsuarios;
        }

        protected DientesLimpiosDbContext()
        {

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (_serviciosUsuarios is not null)
            {
                foreach (var entry in ChangeTracker.Entries<EntidadAuditable>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.FechaCreacion = DateTime.UtcNow;
                            entry.Entity.CreadoPor = _serviciosUsuarios.ObtenerUsuarioId();
                            break;
                        case EntityState.Modified:
                            entry.Entity.UltimaFechaModificacion = DateTime.UtcNow;
                            entry.Entity.UltimaModificacionPor = _serviciosUsuarios.ObtenerUsuarioId();
                            break;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DientesLimpiosDbContext).Assembly);
        }

        public DbSet<Consultorio> Consultorios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Dentista> Dentistas { get; set; }
        public DbSet<Cita> Citas { get; set; }  
    }
}
