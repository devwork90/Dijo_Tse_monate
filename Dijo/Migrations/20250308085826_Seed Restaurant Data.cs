using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dijo.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedRestaurantData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Menu",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "restaurants",
                columns: new[] { "Id", "Address", "created_at", "description", "is_open", "logo_url", "name", "rating", "updated_at" },
                values: new object[,]
                {
                    { new Guid("83b89851-d594-48a0-b66b-94ba920a0c70"), "2282 Samjokozela Street", new DateTime(2025, 2, 15, 14, 30, 0, 0, DateTimeKind.Unspecified), "It's finger lickin good", true, "KFC.jpg", "KFC", 3, null },
                    { new Guid("b7272c86-286d-465c-b993-10e177f6f056"), "11 Songwiqi Street", new DateTime(2025, 3, 12, 14, 10, 0, 0, DateTimeKind.Unspecified), "Where good time is found", true, "ace.jpg", "Kwa Ace", 5, null },
                    { new Guid("c5e708e3-46e5-49ff-bb27-a94cf56fa2fe"), "40 Fairdale Street", new DateTime(2025, 2, 27, 18, 15, 0, 0, DateTimeKind.Unspecified), "WHere good food is", false, "loza.jpg", "Loza's braai vleis", 4, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "restaurants",
                keyColumn: "Id",
                keyValue: new Guid("83b89851-d594-48a0-b66b-94ba920a0c70"));

            migrationBuilder.DeleteData(
                table: "restaurants",
                keyColumn: "Id",
                keyValue: new Guid("b7272c86-286d-465c-b993-10e177f6f056"));

            migrationBuilder.DeleteData(
                table: "restaurants",
                keyColumn: "Id",
                keyValue: new Guid("c5e708e3-46e5-49ff-bb27-a94cf56fa2fe"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Menu",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
