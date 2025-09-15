namespace DientesLimpios.Aplicacion.Contratos.Persitencia
{
    public interface IUnidadDeTrabajo
    {
        //Cuando el conjunto de operaciones son exitosas
        Task Persistir();

        //Revertir las operaciones en caso de que uno operacion falle
        Task Reversar();
    }
}
