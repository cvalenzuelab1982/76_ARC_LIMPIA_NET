using DientesLimpios.Aplicacion.Contratos.Identidad;
using DientesLimpios.Identidad.Modelos;
using DientesLimpios.Identidad.Servicios;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DientesLimpios.Identidad
{
    public static class RegistroDeServicioDeIdentidad
    {
        public static void AgregarServiciosDeIdentidad(this IServiceCollection services)
        {
            services.AddAuthentication(IdentityConstants.BearerScheme).AddBearerToken(IdentityConstants.BearerScheme);
            //services.AddAuthorizationBuilder();
            services.AddAuthorization(opc =>
            {
                opc.AddPolicy("esadmin", politica => politica.RequireClaim("esadmin"));
            });
            services.AddDbContext<DientesLimpiosIdentityDbContext>(opt => opt.UseSqlServer("name=DientesLimpiosConnectionString"));
            services.AddIdentityCore<Usuario>()
                .AddEntityFrameworkStores<DientesLimpiosIdentityDbContext>()
                .AddApiEndpoints();
            services.AddTransient<IServiciosUsuarios, ServiciosUsuarios>();
            services.AddHttpContextAccessor();
        }
    }
}
