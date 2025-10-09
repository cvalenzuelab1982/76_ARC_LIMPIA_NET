using DientesLimpios.API.Dtos.Dentistas;
using DientesLimpios.API.Dtos.Pacientes;
using DientesLimpios.API.Utilidades;
using DientesLimpios.Aplicacion.CasosDeUso.Dentista.Comandos.ActualizarDentista;
using DientesLimpios.Aplicacion.CasosDeUso.Dentista.Comandos.BorrarDentista;
using DientesLimpios.Aplicacion.CasosDeUso.Dentista.Comandos.CrearDentista;
using DientesLimpios.Aplicacion.CasosDeUso.Dentista.Consultas.ObtenerDetalleDentista;
using DientesLimpios.Aplicacion.CasosDeUso.Dentista.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.ActualizarPaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.BorrarPaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerDetallePaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [ApiController]
    [Route("api/dentistas")]
    public class DentistasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DentistasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<DentistaListadoDTO>>> Get([FromQuery] ConsultaObtenerListadoDentistas consulta)
        {
            var resultado = await _mediator.Send(consulta);
            HttpContext.InsertarPaginacionEnCabecera(resultado.Total);
            return resultado.Elementos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DentistaDetalleDTO>> Get(Guid id)
        {
            var consulta = new ConsultaObtenerDetalleDentista() { Id = id };
            var resultado = await _mediator.Send(consulta);
            return resultado;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreaDentistaDTO creaDentistaDTO)
        {
            var cmd = new CmdCrearDentista { Nombre = creaDentistaDTO.Nombre, Email = creaDentistaDTO.Email };
            await _mediator.Send(cmd);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ActualizarDentistaDTO actualizarDentistaDTO)
        {
            var cmd = new CmdActualizarDentista { Id = id, Nombre = actualizarDentistaDTO.Nombre, Email = actualizarDentistaDTO.Email };
            await _mediator.Send(cmd);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var cmd = new CmdBorrarDentista { Id = id };
            await _mediator.Send(cmd);
            return Ok();
        }
    }
}
