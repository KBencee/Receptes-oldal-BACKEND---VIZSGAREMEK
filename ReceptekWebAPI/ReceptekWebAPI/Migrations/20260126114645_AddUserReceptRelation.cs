using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReceptekWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserReceptRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Receptek",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nev = table.Column<string>(type: "TEXT", nullable: false),
                    Leiras = table.Column<string>(type: "TEXT", nullable: false),
                    Hozzavalok = table.Column<string>(type: "TEXT", nullable: false),
                    ElkeszitesiIdo = table.Column<string>(type: "TEXT", nullable: false),
                    NehezsegiSzint = table.Column<string>(type: "TEXT", nullable: false),
                    Likes = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receptek_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receptek_UserId",
                table: "Receptek",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Receptek");
        }
    }
}
