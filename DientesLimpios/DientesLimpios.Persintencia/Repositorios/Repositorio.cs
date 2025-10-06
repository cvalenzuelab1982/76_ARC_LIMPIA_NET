using DientesLimpios.Aplicacion.Contratos.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace DientesLimpios.Persintencia.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly DientesLimpiosDbContext _Context;

        public Repositorio(DientesLimpiosDbContext context)
        {
            _Context = context;
        }

        public Task Actualizar(T entidad)
        {
            _Context.Update(entidad);
            return Task.CompletedTask;
        }

        public Task<T> Agregar(T entidad)
        {
            _Context.Add(entidad);
            return Task.FromResult(entidad);
        }

        public Task Borrar(T entidad)
        {
            _Context.Remove(entidad);
            return Task.CompletedTask;
        }

        public async Task<T?> ObtenerPorId(Guid id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> ObtenerTodos()
        {
            return await _Context.Set<T>().ToListAsync();
        }

        public async Task<int> ObtenerCantidadTotalRegistros()
        {
            return await _Context.Set<T>().CountAsync();
        }
    }
}
