using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class alimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidades", "Detalles", "FechaCreacion", "FechaModificacion", "ImagenURL", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "Alberca, Jacuzzi, Cocina, Sala, Comedor, 2 Recamaras, 2 Baños", "Villa", new DateTime(2023, 12, 20, 16, 30, 12, 109, DateTimeKind.Local).AddTicks(4411), new DateTime(2023, 12, 20, 16, 30, 12, 109, DateTimeKind.Local).AddTicks(4427), "https://www.villas.com.mx/wp-content/uploads/2019/10/1-1.jpg", 100, "Villa 1", 4, 1000.0 },
                    { 2, "Alberca, Jacuzzi, Cocina, Sala, Comedor, 3 Recamaras, 3 Baños", "Villa", new DateTime(2023, 12, 20, 16, 30, 12, 109, DateTimeKind.Local).AddTicks(4432), new DateTime(2023, 12, 20, 16, 30, 12, 109, DateTimeKind.Local).AddTicks(4433), "https://www.villas.com.mx/wp-content/uploads/2019/10/2-1.jpg", 200, "Villa 2", 6, 2000.0 },
                    { 3, "Alberca, Jacuzzi, Cocina, Sala, Comedor, 4 Recamaras, 4 Baños", "Villa", new DateTime(2023, 12, 20, 16, 30, 12, 109, DateTimeKind.Local).AddTicks(4437), new DateTime(2023, 12, 20, 16, 30, 12, 109, DateTimeKind.Local).AddTicks(4438), "https://www.villas.com.mx/wp-content/uploads/2019/10/3-1.jpg", 300, "Villa 3", 8, 3000.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
