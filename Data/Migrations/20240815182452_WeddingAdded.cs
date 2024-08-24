using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class WeddingAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weddings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartnerId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PartnerId2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WeddingEvent_Date = table.Column<DateOnly>(type: "date", nullable: true),
                    WeddingEvent_Place_City = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    WeddingEvent_Place_Coordinates = table.Column<Point>(type: "geometry", nullable: true),
                    WeddingEvent_Place_Country = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weddings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weddings_FamilyMembers_PartnerId1",
                        column: x => x.PartnerId1,
                        principalTable: "FamilyMembers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Weddings_FamilyMembers_PartnerId2",
                        column: x => x.PartnerId2,
                        principalTable: "FamilyMembers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weddings_PartnerId1",
                table: "Weddings",
                column: "PartnerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Weddings_PartnerId2",
                table: "Weddings",
                column: "PartnerId2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weddings");
        }
    }
}
