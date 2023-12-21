using System.Linq.Expressions;

namespace MagicVilla_API.Repositorio.IRepositorio
{
    public interface IRepository<T> where T : class
    {
        Task Crear(T entidad);
        Task<List<T>> getAll(Expression<Func<T, bool>> filtro = null); //Expression se ejecuta en la base de datos, Func representa una funcion lambda que recibe un T y
                                                                       //devuelve un bool representa una funcion que recibe una entidad y segun la funcion lo retorna o no 
                                                                       //Action representa una funcion que recibe una entidad y no devuelve nada
        Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true);
        Task Remover(T entidad);
        Task Guardar();

    }
}
