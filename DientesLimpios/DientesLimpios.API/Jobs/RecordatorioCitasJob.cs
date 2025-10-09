
using DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.EnviarRecordatorioCitas;
using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.API.Jobs
{
    public class RecordatorioCitasJob : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TimeZoneInfo _zonaHorarioPE = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");

        public RecordatorioCitasJob(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) 
            {
                var ahora = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _zonaHorarioPE);

                if (ahora.Hour == 8)
                {
                    using var scope = _scopeFactory.CreateScope();
                    var medidor = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await medidor.Send(new CmdEnviarRecordatorioCitas());
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken); 
            }
        }
    }
}
