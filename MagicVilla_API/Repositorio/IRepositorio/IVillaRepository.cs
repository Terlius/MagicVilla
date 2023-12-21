using MagicVilla_API.Models;

namespace MagicVilla_API.Repositorio.IRepositorio
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task<Villa> Actualizar(Villa villa);
    }
}
