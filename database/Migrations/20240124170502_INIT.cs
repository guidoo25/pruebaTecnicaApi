using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace database.Migrations
{
    /// <inheritdoc />
    public partial class INIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Age = table.Column<short>(type: "smallint", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    AllowedAge = table.Column<short>(type: "smallint", nullable: false),
                    LengthMinutes = table.Column<short>(type: "smallint", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Number = table.Column<short>(type: "smallint", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillboardEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillboardEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillboardEntities_MovieEntities_MovieId",
                        column: x => x.MovieId,
                        principalTable: "MovieEntities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillboardEntities_RoomEntities_RoomId",
                        column: x => x.RoomId,
                        principalTable: "RoomEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SeatEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<short>(type: "smallint", nullable: false),
                    RowNumber = table.Column<short>(type: "smallint", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatEntities_RoomEntities_RoomId",
                        column: x => x.RoomId,
                        principalTable: "RoomEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookingEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<int>(type: "int", nullable: false),
                    BillboardId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingEntities_BillboardEntities_BillboardId",
                        column: x => x.BillboardId,
                        principalTable: "BillboardEntities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookingEntities_CustomerEntities_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerEntities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookingEntities_SeatEntities_SeatId",
                        column: x => x.SeatId,
                        principalTable: "SeatEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillboardEntities_MovieId",
                table: "BillboardEntities",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_BillboardEntities_RoomId",
                table: "BillboardEntities",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingEntities_BillboardId",
                table: "BookingEntities",
                column: "BillboardId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingEntities_CustomerId",
                table: "BookingEntities",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingEntities_SeatId",
                table: "BookingEntities",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatEntities_RoomId",
                table: "SeatEntities",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingEntities");

            migrationBuilder.DropTable(
                name: "BillboardEntities");

            migrationBuilder.DropTable(
                name: "CustomerEntities");

            migrationBuilder.DropTable(
                name: "SeatEntities");

            migrationBuilder.DropTable(
                name: "MovieEntities");

            migrationBuilder.DropTable(
                name: "RoomEntities");
        }
    }
}
