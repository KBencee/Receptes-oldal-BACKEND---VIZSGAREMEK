using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReceptekWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Mentett : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MentettReceptek",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReceptId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MentveEkkor = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReceptId1 = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MentettReceptek", x => new { x.UserId, x.ReceptId });
                    table.ForeignKey(
                        name: "FK_MentettReceptek_Receptek_ReceptId",
                        column: x => x.ReceptId,
                        principalTable: "Receptek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MentettReceptek_Receptek_ReceptId1",
                        column: x => x.ReceptId1,
                        principalTable: "Receptek",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MentettReceptek_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MentettReceptek_ReceptId",
                table: "MentettReceptek",
                column: "ReceptId");

            migrationBuilder.CreateIndex(
                name: "IX_MentettReceptek_ReceptId1",
                table: "MentettReceptek",
                column: "ReceptId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MentettReceptek");
        }
    }
}
