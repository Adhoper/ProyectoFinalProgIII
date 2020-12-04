using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoFinalProgIII.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Facturacion_ClienteId",
                table: "Facturacion",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facturacion_Clientes_ClienteId",
                table: "Facturacion",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facturacion_Clientes_ClienteId",
                table: "Facturacion");

            migrationBuilder.DropIndex(
                name: "IX_Facturacion_ClienteId",
                table: "Facturacion");
        }
    }
}
