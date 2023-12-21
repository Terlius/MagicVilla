using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Repositorio.IRepositorio;

namespace MagicVilla_API.Repositorio
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDBContext _db;

        public VillaRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Villa> Actualizar(Villa villa)
        {
            villa.FechaModificacion = DateTime.Now;
            _db.Villas.Update(villa);
            await Guardar();
            return villa;
        }
    }
}
