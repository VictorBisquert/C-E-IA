using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estadistico_Web.Migrations
{
    public partial class AddUpdatedAtToScale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "scales",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "scales");
        }
    }
}
