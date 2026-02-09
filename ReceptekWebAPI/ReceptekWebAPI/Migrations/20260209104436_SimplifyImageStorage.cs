using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReceptekWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyImageStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeltoltottKepek");

            migrationBuilder.AddColumn<string>(
                name: "KepFileId",
                table: "Receptek",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KepUrl",
                table: "Receptek",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KepFileId",
                table: "Receptek");

            migrationBuilder.DropColumn(
                name: "KepUrl",
                table: "Receptek");

            migrationBuilder.CreateTable(
                name: "FeltoltottKepek",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReceptId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FileId = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeltoltottKepek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeltoltottKepek_Receptek_ReceptId",
                        column: x => x.ReceptId,
                        principalTable: "Receptek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeltoltottKepek_ReceptId",
                table: "FeltoltottKepek",
                column: "ReceptId",
                unique: true);
        }
    }
}
