using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control_Estadistico_Web.Migrations
{
    /// <inheritdoc />
    public partial class ajusteCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "companies",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "companies",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "companies",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "logo",
                table: "companies",
                newName: "Logo");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "companies",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "companies",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "companies",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "cif",
                table: "companies",
                newName: "Cif");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "companies",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "companies",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "companies",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "companies",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "companies",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "companies",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Logo",
                table: "companies",
                newName: "logo");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "companies",
                newName: "location");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "companies",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "companies",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "Cif",
                table: "companies",
                newName: "cif");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "companies",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "companies",
                newName: "active");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "companies",
                newName: "id");
        }
    }
}
