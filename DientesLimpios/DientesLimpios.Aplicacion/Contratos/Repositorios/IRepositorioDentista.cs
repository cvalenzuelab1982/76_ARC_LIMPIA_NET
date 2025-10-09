using DientesLimpios.Aplicacion.CasosDeUso.Dentista.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Dominio.Entidades;

namespace DientesLimpios.Aplicacion.Contratos.Repositorios
{
    public interface IRepositorioDentista : IRepositorio<Dentista>
    {
        Task<IEnumerable<Dentista>> ObtenerFiltrado(FiltroDentistaDTO filtro);
    }
}
