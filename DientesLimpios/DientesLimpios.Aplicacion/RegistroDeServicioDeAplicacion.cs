﻿using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.Extensions.DependencyInjection;

namespace DientesLimpios.Aplicacion
{
    public static class RegistroDeServicioDeAplicacion
    {
        public static IServiceCollection AgregarServiciosDeAplicacion(this IServiceCollection services)
        {
            services.AddTransient<IMediator, MediadorSimple>();
            services.Scan(scan => scan.FromAssembliesOf(typeof(IMediator))
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            //services.AddScoped<IRequestHandler<CmdCrearConsultorio, Guid>, CuCrearConsultorio>();
            //services.AddScoped<IRequestHandler<ConsultaObtenerDetalleConsultorio, ConsultorioDetalleDTO>, CuObtenerDetalleConsultorio>();
            //services.AddScoped<IRequestHandler<ConsultaObtenerListadoConsultorios, List<ConsultorioListadoDTO>>, CuObtenerListadoConsultorios>();
            //services.AddScoped<IRequestHandler<CmdActualizarConsultorio>,CuActualizarConsultorio>();
            //services.AddScoped<IRequestHandler<CmdBorrarConsultorio>, CuBorrarConsultorio>();

            return services;
        }
    }
}
