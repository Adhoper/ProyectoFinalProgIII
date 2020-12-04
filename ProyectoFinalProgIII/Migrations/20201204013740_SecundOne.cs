using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoFinalProgIII.Migrations
{
    public partial class SecundOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacturacionProductos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductosId = table.Column<Guid>(nullable: false),
                    FacturacionId = table.Column<Guid>(nullable: false),
                    PrecioTotal = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturacionProductos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturacionProductos_Facturacion_FacturacionId",
                        column: x => x.FacturacionId,
                        principalTable: "Facturacion",
                        principalColumn: "FacturacionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacturacionProductos_Productos_ProductosId",
                        column: x => x.ProductosId,
                        principalTable: "Productos",
                        principalColumn: "ProductosId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacturacionProductos_FacturacionId",
                table: "FacturacionProductos",
                column: "FacturacionId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturacionProductos_ProductosId",
                table: "FacturacionProductos",
                column: "ProductosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacturacionProductos");
        }
    }
}
