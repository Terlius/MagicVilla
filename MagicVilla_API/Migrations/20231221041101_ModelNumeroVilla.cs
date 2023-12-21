using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class ModelNumeroVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeroVillas",
                columns: table => new
                {
                    Numero = table.Column<int>(type: "int", nullable: false),
                    IdVilla = table.Column<int>(type: "int", nullable: false),
                    Detalles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroVillas", x => x.Numero);
                    table.ForeignKey(
                        name: "FK_NumeroVillas_Villas_IdVilla",
                        column: x => x.IdVilla,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaModificacion" },
                values: new object[] { new DateTime(2023, 12, 20, 23, 11, 1, 462, DateTimeKind.Local).AddTicks(3094), new DateTime(2023, 12, 20, 23, 11, 1, 462, DateTimeKind.Local).AddTicks(3109) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaModificacion" },
                values: new object[] { new DateTime(2023, 12, 20, 23, 11, 1, 462, DateTimeKind.Local).AddTicks(3183), new DateTime(2023, 12, 20, 23, 11, 1, 462, DateTimeKind.Local).AddTicks(3185) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreacion", "FechaModificacion" },
                values: new object[] { new DateTime(2023, 12, 20, 23, 11, 1, 462, DateTimeKind.Local).AddTicks(3190), new DateTime(2023, 12, 20, 23, 11, 1, 462, DateTimeKind.Local).AddTicks(3191) });

            migrationBuilder.CreateIndex(
                name: "IX_NumeroVillas_IdVilla",
                table: "NumeroVillas",
                column: "IdVilla");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroVillas");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaModificacion" },
                values: new object[] { new DateTime(2023, 12, 20, 16, 30, 12, 109, DateTimeKind.Local).AddTicks(4411), new DateTime(2023, 12, 20, 16, 30, 12, 109, DateTimeKind.Local).AddTicks(4427) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaModificacion" },
                values: new object[] { new DateTime(2023, 12, 20, 16, 30, 12, 109, DateTimeKind.Local).AddTicks(4432), new DateTime(2023, 12, 20, 16, 30, 12, 109, DateTimeKind.Local).AddTicks(4433) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreacion", "FechaModificacion" },
                values: new object[] { new DateTime(2023, 12, 20, 16, 30, 12, 109, DateTimeKind.Local).AddTicks(4437), new DateTime(2023, 12, 20, 16, 30, 12, 109, DateTimeKind.Local).AddTicks(4438) });
        }
    }
}
