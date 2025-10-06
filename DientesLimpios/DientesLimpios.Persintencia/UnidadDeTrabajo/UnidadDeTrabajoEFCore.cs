using DientesLimpios.Aplicacion.Contratos.Persitencia;

namespace DientesLimpios.Persintencia.UnidadDeTrabajo
{
    public class UnidadDeTrabajoEFCore : IUnidadDeTrabajo
    {
        private readonly DientesLimpiosDbContext _Context;

        public UnidadDeTrabajoEFCore(DientesLimpiosDbContext context)
        {
            _Context = context;
        }

        public async Task Persistir()
        {
            await _Context.SaveChangesAsync();
        }

        public Task Reversar()
        {
            return Task.CompletedTask;
        }
    }
}
