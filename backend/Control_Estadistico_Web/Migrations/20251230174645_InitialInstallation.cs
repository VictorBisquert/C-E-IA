using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estadistico_Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialInstallation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_companies_CompanyId",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_CompanyId",
                table: "Invitations");

            migrationBuilder.AddColumn<Guid>(
                name: "InstallationId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "installations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poblacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_installations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "installations");

            migrationBuilder.DropColumn(
                name: "InstallationId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_CompanyId",
                table: "Invitations",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_companies_CompanyId",
                table: "Invitations",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
