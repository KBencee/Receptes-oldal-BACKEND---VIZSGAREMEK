using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReceptekWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Fix_MentettRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MentettReceptek_Receptek_ReceptId1",
                table: "MentettReceptek");

            migrationBuilder.DropIndex(
                name: "IX_MentettReceptek_ReceptId1",
                table: "MentettReceptek");

            migrationBuilder.DropColumn(
                name: "ReceptId1",
                table: "MentettReceptek");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReceptId1",
                table: "MentettReceptek",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MentettReceptek_ReceptId1",
                table: "MentettReceptek",
                column: "ReceptId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MentettReceptek_Receptek_ReceptId1",
                table: "MentettReceptek",
                column: "ReceptId1",
                principalTable: "Receptek",
                principalColumn: "Id");
        }
    }
}
