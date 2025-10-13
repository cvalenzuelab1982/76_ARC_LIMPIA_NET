using DientesLimpios.Dominio.Comunes;
using DientesLimpios.Dominio.Enums;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosValor;

namespace DientesLimpios.Dominio.Entidades
{
    public class Cita : EntidadAuditable
    {
        public Guid Id { get; private set; }
        public Guid PacienteId { get; private set; }
        public Guid DentistaId { get; private set; }
        public Guid ConsultorioId { get; private set; }
        public EstadoCita Estado { get; private set; }
        public IntervaloDeTiempo IntervaloDeTiempo { get; private set; } = null!;
        public Paciente? Paciente { get; private set; }
        public Dentista? Dentista { get; private set; }
        public Consultorio? Consultorio { get; private set; }

        private Cita()
        {
            
        }

        public Cita(Guid pacienteId, Guid dentistadId, Guid consultorioId, IntervaloDeTiempo intervaloDeTiempo)
        {
            var horaLocal = DateTime.Now;
            if (intervaloDeTiempo.Inicio < horaLocal)
            {
                throw new ExcepcionDeReglaDeNegocio($"La fecha de inicio no puede ser anterior a la fecha actual");
            }

            Id = Guid.CreateVersion7();
            PacienteId = pacienteId;
            DentistaId = dentistadId;
            ConsultorioId = consultorioId;
            IntervaloDeTiempo = intervaloDeTiempo;
            Estado = EstadoCita.Programada;
        }

        public void Cancelar()
        {
            if (Estado != EstadoCita.Programada)
            {
                throw new ExcepcionDeReglaDeNegocio("Solo se pueden cancelar citas programadas.");
            }

            Estado = EstadoCita.Cancelada;
        }

        public void Completar()
        {
            if (Estado != EstadoCita.Programada)
            {
                throw new ExcepcionDeReglaDeNegocio("Solo se pueden completar citas programadas.");
            }

            Estado = EstadoCita.Completada;
        }
    }
}
