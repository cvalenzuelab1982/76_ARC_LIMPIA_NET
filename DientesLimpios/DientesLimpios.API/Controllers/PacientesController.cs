using DientesLimpios.API.Dtos.Pacientes;
using DientesLimpios.API.Utilidades;
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
    [Route("api/pacientes")]
    public class PacientesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PacientesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<PacienteListadoDTO>>> Get([FromQuery] ConsultaObtenerListadoPacientes consulta)
        {
            var resultado = await _mediator.Send(consulta);
            HttpContext.InsertarPaginacionEnCabecera(resultado.Total);
            return resultado.Elementos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDetalleDTO>> Get(Guid id)
        {
            var consulta = new ConsultaObtenerDetallePaciente() { Id = id };
            var resultado = await _mediator.Send(consulta);
            return resultado;
        }

        [HttpPost]
        public async Task<IActionResult>Post(CreaPacienteDTO creaPacienteDTO)
        {
            var cmd = new CmdCrearPaciente { Nombre = creaPacienteDTO.Nombre, Email = creaPacienteDTO.Email };
            await _mediator.Send(cmd);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ActualizarPacienteDTO actualizarPacienteDTO)
        {
            var cmd = new CmdActualizarPaciente { Id = id,Nombre=actualizarPacienteDTO.Nombre, Email= actualizarPacienteDTO.Email};
            await _mediator.Send(cmd);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var cmd = new CmdBorrarPaciente { Id = id};
            await _mediator.Send(cmd);
            return Ok();
        }
    }
}
