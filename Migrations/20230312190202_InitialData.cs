using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace entityapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "Descripcion", "Nombre", "Peso" },
                values: new object[,]
                {
                    { new Guid("771167b7-6e0b-429c-85ad-de56cddab002"), null, "Actividades Personales", 50 },
                    { new Guid("771167b7-6e0b-429c-85ad-de56cddab0c4"), null, "Actividades Pendientes", 20 }
                });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaId", "CategoriaId", "Descripcion", "FechaCreacion", "PrioridadTarea", "Titulo" },
                values: new object[] { new Guid("771167b7-6e0b-429c-85ad-de56cddab010"), new Guid("771167b7-6e0b-429c-85ad-de56cddab0c4"), null, new DateTime(2023, 3, 12, 13, 2, 2, 298, DateTimeKind.Local).AddTicks(746), 1, "Pago de Servicios Públicos" });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaId", "CategoriaId", "Descripcion", "Estatus", "FechaCreacion", "PrioridadTarea", "Titulo" },
                values: new object[] { new Guid("771167b7-6e0b-429c-85ad-de56cddab011"), new Guid("771167b7-6e0b-429c-85ad-de56cddab002"), null, 1, new DateTime(2023, 3, 12, 13, 2, 2, 298, DateTimeKind.Local).AddTicks(763), 0, "Terminar de ver pelicula en Netflix" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("771167b7-6e0b-429c-85ad-de56cddab010"));

            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("771167b7-6e0b-429c-85ad-de56cddab011"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("771167b7-6e0b-429c-85ad-de56cddab002"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("771167b7-6e0b-429c-85ad-de56cddab0c4"));
        }
    }
}
