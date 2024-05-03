using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class DeathInsteadOfDeathDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeathDate",
                table: "FamilyMembers");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Death_Date",
                table: "FamilyMembers",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Death_Place",
                table: "FamilyMembers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Death_Date",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "Death_Place",
                table: "FamilyMembers");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeathDate",
                table: "FamilyMembers",
                type: "datetime2",
                nullable: true);
        }
    }
}
