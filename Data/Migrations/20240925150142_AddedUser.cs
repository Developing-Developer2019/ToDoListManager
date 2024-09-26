using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Todo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDateT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Todo_AppUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "Name" },
                values: new object[] { "dff66c34-bd9b-4355-80ba-e7c7cd02d83f", "Example User" });

            migrationBuilder.InsertData(
                table: "Todo",
                columns: new[] { "Id", "Description", "DueDateT", "IsCompleted", "Priority", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "Some Description", new DateTime(2024, 9, 27, 16, 1, 42, 581, DateTimeKind.Local).AddTicks(6476), false, 2, "Task 1", "dff66c34-bd9b-4355-80ba-e7c7cd02d83f" },
                    { 2, "Some Description", new DateTime(2024, 10, 1, 16, 1, 42, 581, DateTimeKind.Local).AddTicks(6535), false, 1, "Task 2", "dff66c34-bd9b-4355-80ba-e7c7cd02d83f" },
                    { 3, "Some Description", new DateTime(2024, 9, 29, 16, 1, 42, 581, DateTimeKind.Local).AddTicks(6537), false, 3, "Task 3", "dff66c34-bd9b-4355-80ba-e7c7cd02d83f" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todo_UserId",
                table: "Todo",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todo");

            migrationBuilder.DropTable(
                name: "AppUser");
        }
    }
}
