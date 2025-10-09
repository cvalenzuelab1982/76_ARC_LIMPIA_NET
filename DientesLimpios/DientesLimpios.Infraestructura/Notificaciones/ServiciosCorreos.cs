using DientesLimpios.Aplicacion.Contratos.Notificaciones;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Net;
using System.Net.Mail;

namespace DientesLimpios.Infraestructura.Notificaciones
{
    public class ServiciosCorreos : IServicioNotificaciones
    {
        private readonly IConfiguration _configuration;

        public ServiciosCorreos(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarConfirmacionCita(ConfirmacionCitaDTO cita)
        {
            var asunto = "Confirmacion de cita - Dientes Limpios";
            var cuerpo = $"""
            Estimado (a) {cita.Paciente}

            Su cita con el Dr (Dra.) {cita.Dentista} has sido programada para el {cita.Fecha.ToString("f", new CultureInfo("es-PE"))} en el consultorio {cita.Consultorio}.

            !Le esperamos!

            Equipo Dientes Limpios
            """;

            await EnviarMensaje(cita.Paciente_Email, asunto, cuerpo);
        }

        public async Task EnviarRecordatorioCita(RecordatorioCitaDTO cita)
        {
            var asunto = "RECORDATORIO: Confirmacion de cita - Dientes Limpios";
            var cuerpo = $"""
            Estimado (a) {cita.Paciente}

            Le recordamos que tiene una cita con el Dr (Dra.) {cita.Dentista} para el {cita.Fecha.ToString("f", new CultureInfo("es-PE"))} en el consultorio {cita.Consultorio}.

            !Le esperamos!

            Equipo Dientes Limpios
            """;

            await EnviarMensaje(cita.Paciente_Email, asunto, cuerpo);
        }

        private async Task EnviarMensaje(string emailDestinatario, string asunto, string cuerpo)
        {
            var nuestroEmail = _configuration.GetValue<string>("CONFIGURACION_EMAIL:EMAIL");
            var password = _configuration.GetValue<string>("CONFIGURACION_EMAIL:PASSWORD");
            var host = _configuration.GetValue<string>("CONFIGURACION_EMAIL:HOST");
            var puerto = _configuration.GetValue<int>("CONFIGURACION_EMAIL:PUERTO");

            var smtpCliente = new SmtpClient(host, puerto);
            smtpCliente.EnableSsl = true;
            smtpCliente.UseDefaultCredentials = false;
            smtpCliente.Credentials = new NetworkCredential(nuestroEmail, password);

            var mensaje = new MailMessage(nuestroEmail!, emailDestinatario, asunto, cuerpo);
            await smtpCliente.SendMailAsync(mensaje);
        }
    }
}
