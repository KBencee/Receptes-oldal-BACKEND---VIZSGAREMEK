using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReceptekWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddReceptKommentek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReceptKommentek",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Szoveg = table.Column<string>(type: "TEXT", nullable: false),
                    IrtaEkkor = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReceptId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptKommentek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceptKommentek_Receptek_ReceptId",
                        column: x => x.ReceptId,
                        principalTable: "Receptek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceptKommentek_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceptKommentek_ReceptId",
                table: "ReceptKommentek",
                column: "ReceptId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptKommentek_UserId",
                table: "ReceptKommentek",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceptKommentek");
        }
    }
}
