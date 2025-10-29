using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategory", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "EventStatus",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventStatus", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "CategoryType",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventCategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryType", x => x.TypeId);
                    table.ForeignKey(
                        name: "FK_CategoryType_EventCategory_EventCategoryId",
                        column: x => x.EventCategoryId,
                        principalTable: "EventCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserToken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    BannerImageUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ThemeColor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "#FFFFFF"),
                    EventStatusStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Event_CategoryType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "CategoryType",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_EventCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "EventCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_EventStatus_EventStatusStatusId",
                        column: x => x.EventStatusStatusId,
                        principalTable: "EventStatus",
                        principalColumn: "StatusId");
                    table.ForeignKey(
                        name: "FK_Event_EventStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "EventStatus",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventSector",
                columns: table => new
                {
                    EventSectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSector", x => x.EventSectorId);
                    table.ForeignKey(
                        name: "FK_EventSector_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EventCategory",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Music" },
                    { 2, "Stand-up" },
                    { 3, "Conference" }
                });

            migrationBuilder.InsertData(
                table: "EventStatus",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { 1, "Scheduled" },
                    { 2, "Active" },
                    { 3, "Postponed" },
                    { 4, "Finished" }
                });

            migrationBuilder.InsertData(
                table: "CategoryType",
                columns: new[] { "TypeId", "EventCategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Rock" },
                    { 2, 1, "Pop" },
                    { 3, 1, "Trap" },
                    { 4, 1, "Reggaeton" },
                    { 5, 1, "Electronic" },
                    { 6, 1, "Metal" },
                    { 7, 1, "Cumbia" },
                    { 8, 1, "Hip-Hop" },
                    { 9, 2, "Comedy" },
                    { 10, 2, "Satire" },
                    { 11, 3, "Technology" },
                    { 12, 3, "Business" },
                    { 13, 3, "Education" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryType_EventCategoryId",
                table: "CategoryType",
                column: "EventCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_CategoryId",
                table: "Event",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_CategoryId_TypeId",
                table: "Event",
                columns: new[] { "CategoryId", "TypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_Event_EventStatusStatusId",
                table: "Event",
                column: "EventStatusStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_StatusId",
                table: "Event",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_TypeId",
                table: "Event",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_UserToken",
                table: "Event",
                column: "UserToken");

            migrationBuilder.CreateIndex(
                name: "IX_Event_VenueId",
                table: "Event",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSector_EventId",
                table: "EventSector",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSector_SectorId",
                table: "EventSector",
                column: "SectorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventSector");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "CategoryType");

            migrationBuilder.DropTable(
                name: "EventStatus");

            migrationBuilder.DropTable(
                name: "EventCategory");
        }
    }
}
