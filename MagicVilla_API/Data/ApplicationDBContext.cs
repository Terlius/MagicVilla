using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<NumeroVilla> NumeroVillas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                //genera 3
                new Villa
                {
                    Id = 1,
                    Nombre = "Villa 1",
                    Detalles = "Villa",
                    Tarifa = 1000,
                    Ocupantes = 4,
                    MetrosCuadrados = 100,
                    ImagenURL = "https://www.villas.com.mx/wp-content/uploads/2019/10/1-1.jpg",
                    Amenidades = "Alberca, Jacuzzi, Cocina, Sala, Comedor, 2 Recamaras, 2 Baños",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                },
                new Villa
                {
                    Id = 2,
                    Nombre = "Villa 2",
                    Detalles = "Villa",
                    Tarifa = 2000,
                    Ocupantes = 6,
                    MetrosCuadrados = 200,
                    ImagenURL = "https://www.villas.com.mx/wp-content/uploads/2019/10/2-1.jpg",
                    Amenidades = "Alberca, Jacuzzi, Cocina, Sala, Comedor, 3 Recamaras, 3 Baños",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                },
                new Villa
                {
                    Id = 3,
                    Nombre = "Villa 3",
                    Detalles = "Villa",
                    Tarifa = 3000,
                    Ocupantes = 8,
                    MetrosCuadrados = 300,
                    ImagenURL = "https://www.villas.com.mx/wp-content/uploads/2019/10/3-1.jpg",
                    Amenidades = "Alberca, Jacuzzi, Cocina, Sala, Comedor, 4 Recamaras, 4 Baños",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                }

                );
        }


    }
}
