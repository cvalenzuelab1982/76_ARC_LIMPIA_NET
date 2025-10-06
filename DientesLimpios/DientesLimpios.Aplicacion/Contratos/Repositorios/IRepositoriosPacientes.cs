using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Dominio.Entidades;

namespace DientesLimpios.Aplicacion.Contratos.Repositorios
{
    public interface IRepositoriosPacientes : IRepositorio<Paciente>
    {
        Task<IEnumerable<Paciente>> ObtenerFiltrado(FiltroPacienteDTO filtro);
    }
}
