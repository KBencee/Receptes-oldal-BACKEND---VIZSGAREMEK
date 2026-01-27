using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReceptekWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class ManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cimkek",
                columns: table => new
                {
                    CimkeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CimkeNev = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cimkek", x => x.CimkeId);
                });

            migrationBuilder.CreateTable(
                name: "ReceptCimkek",
                columns: table => new
                {
                    ReceptId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CimkeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptCimkek", x => new { x.ReceptId, x.CimkeId });
                    table.ForeignKey(
                        name: "FK_ReceptCimkek_Cimkek_CimkeId",
                        column: x => x.CimkeId,
                        principalTable: "Cimkek",
                        principalColumn: "CimkeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceptCimkek_Receptek_ReceptId",
                        column: x => x.ReceptId,
                        principalTable: "Receptek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceptCimkek_CimkeId",
                table: "ReceptCimkek",
                column: "CimkeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceptCimkek");

            migrationBuilder.DropTable(
                name: "Cimkek");
        }
    }
}
