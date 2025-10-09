using DientesLimpios.Aplicacion.CasosDeUso.Dentista.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Persintencia.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace DientesLimpios.Persintencia.Repositorios
{
    public class RepositorioDentista : Repositorio<Dentista>, IRepositorioDentista
    {
        private readonly DientesLimpiosDbContext _context;

        public RepositorioDentista(DientesLimpiosDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dentista>> ObtenerFiltrado(FiltroDentistaDTO filtro)
        {
            var queryable = _context.Dentistas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
            {
                queryable = queryable.Where(x => x.Nombre.Contains(filtro.Nombre));
            }

            if (!string.IsNullOrWhiteSpace(filtro.Email))
            {
                queryable = queryable.Where(x => x.Email.Valor.Contains(filtro.Email));
            }

            return await queryable.OrderBy(x => x.Nombre)
                .Paginar(filtro.Pagina, filtro.RegistroPorPagina).ToListAsync();
        }
    }
}
