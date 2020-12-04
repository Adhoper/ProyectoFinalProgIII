using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoFinalProgIII.Migrations
{
    public partial class precio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Itbis",
                table: "Facturacion",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "Facturacion",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Facturacion");

            migrationBuilder.AlterColumn<string>(
                name: "Itbis",
                table: "Facturacion",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal));
        }
    }
}
