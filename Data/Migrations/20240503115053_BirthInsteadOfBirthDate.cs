using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class BirthInsteadOfBirthDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "FamilyMembers",
                newName: "Birth_Date");

            migrationBuilder.AddColumn<string>(
                name: "Birth_Place",
                table: "FamilyMembers",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birth_Place",
                table: "FamilyMembers");

            migrationBuilder.RenameColumn(
                name: "Birth_Date",
                table: "FamilyMembers",
                newName: "BirthDate");
        }
    }
}
