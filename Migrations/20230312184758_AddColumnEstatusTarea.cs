using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace entityapi.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnEstatusTarea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estatus",
                table: "Tarea",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estatus",
                table: "Tarea");
        }
    }
}
