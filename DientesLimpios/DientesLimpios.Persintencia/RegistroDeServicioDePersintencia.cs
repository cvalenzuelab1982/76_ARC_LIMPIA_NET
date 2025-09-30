using DientesLimpios.Aplicacion.Contratos.Persitencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Persintencia.Repositorios;
using DientesLimpios.Persintencia.UnidadDeTrabajo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DientesLimpios.Persintencia
{
    public static class RegistroDeServicioDePersintencia
    {
        public static IServiceCollection AgregarServiciosDePersistencia(this IServiceCollection services)
        {
            services.AddDbContext<DientesLimpiosDbContext>(options => options.UseSqlServer("name=DientesLimpiosConnectionString"));

            //
            services.AddScoped<IRepositorioConsultorios, RepositorioConsultorios>();
            services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajoEFCore>();

            return services;
        }
    }
}
