using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estadistico_Web.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInstallationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Poblacion",
                table: "installations",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "Direccion",
                table: "installations",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "Ciudad",
                table: "installations",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "installations",
                newName: "Poblacion");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "installations",
                newName: "Direccion");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "installations",
                newName: "Ciudad");
        }
    }
}
