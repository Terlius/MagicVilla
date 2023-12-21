using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Repositorio.IRepositorio;

namespace MagicVilla_API.Repositorio
{
    public class NumeroVillaRepository : Repository<NumeroVilla>, INumeroVillaRepository
    {
        private readonly ApplicationDBContext _db;

        public NumeroVillaRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public async Task<NumeroVilla> Actualizar(NumeroVilla numeroVilla)
        {
            numeroVilla.FechaModificacion = DateTime.Now;
            _db.NumeroVillas.Update(numeroVilla);
            await Guardar();
            return numeroVilla;
        }
    }
}
