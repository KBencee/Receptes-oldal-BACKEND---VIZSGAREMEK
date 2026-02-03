using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReceptekWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFeltoltveEkkorToRecept : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FeltoltveEkkor",
                table: "Receptek",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeltoltveEkkor",
                table: "Receptek");
        }
    }
}
