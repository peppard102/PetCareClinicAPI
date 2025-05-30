using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareClinicAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDoBtoDateOfBirth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DoB",
                table: "Pets",
                newName: "DateOfBirth");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Pets",
                newName: "DoB");
        }
    }
}
