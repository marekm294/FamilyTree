using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class PlaceAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birth_Place",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "Death_Place",
                table: "FamilyMembers");

            migrationBuilder.AddColumn<string>(
                name: "Birth_Place_City",
                table: "FamilyMembers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<Point>(
                name: "Birth_Place_Coordinates",
                table: "FamilyMembers",
                type: "geometry",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Birth_Place_Country",
                table: "FamilyMembers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Death_Place_City",
                table: "FamilyMembers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<Point>(
                name: "Death_Place_Coordinates",
                table: "FamilyMembers",
                type: "geometry",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Death_Place_Country",
                table: "FamilyMembers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birth_Place_City",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "Birth_Place_Coordinates",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "Birth_Place_Country",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "Death_Place_City",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "Death_Place_Coordinates",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "Death_Place_Country",
                table: "FamilyMembers");

            migrationBuilder.AddColumn<string>(
                name: "Birth_Place",
                table: "FamilyMembers",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Death_Place",
                table: "FamilyMembers",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);
        }
    }
}
