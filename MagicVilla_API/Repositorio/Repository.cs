using MagicVilla_API.Data;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace MagicVilla_API.Repositorio
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDBContext db )
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            
        }


        public async Task Crear(T entidad)
        {
            await dbSet.AddAsync(entidad);
            await Guardar();
           
        }

        public async Task<List<T>> getAll(Expression<Func<T, bool>> filtro = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
           
            return await query.ToListAsync();

        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
            
        }

        public async Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true)
        {
           IQueryable<T> query = dbSet; //Al inicializar query con dbSet, estás comenzando con una consulta que abarca todos los elementos de la entidad T.
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            if(!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task Remover(T entidad)
        {
           dbSet.Remove(entidad);
            await Guardar();
        }
    }
}
