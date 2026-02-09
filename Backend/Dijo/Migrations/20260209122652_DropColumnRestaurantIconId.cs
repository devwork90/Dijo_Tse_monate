using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class DropColumnRestaurantIconId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "FileExtension", "FileName", "FilePath", "FileSizeInBytes", "ImageType", "MenuId", "RestaurantId", "created_at", "updated_at" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-1234-56789abcde01"), ".jpeg", "hungry_lion.jpeg", "https://localhost:7065/Images/RestaurantIcon/hungry_lion.jpeg", 396632L, 2, null, null, new DateTime(2026, 1, 9, 21, 45, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("a2b3c4d5-e6f7-7890-1234-56789abcde07"), ".jpeg", "debonairs.jpeg", "https://localhost:7065/Images/RestaurantIcon/debonairs.jpeg", 382259L, 2, null, null, new DateTime(2026, 1, 9, 22, 15, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("b1c2d3e4-f5a6-7890-1234-56789abcde02"), ".jpeg", "kfc.jpeg", "https://localhost:7065/Images/RestaurantIcon/kfc.jpeg", 377633L, 2, null, null, new DateTime(2026, 1, 9, 21, 50, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("b2c3d4e5-f6a7-7890-1234-56789abcde08"), ".jpeg", "romans.jpeg", "https://localhost:7065/Images/RestaurantIcon/romans.jpeg", 392519L, 2, null, null, new DateTime(2026, 1, 9, 22, 20, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("c1d2e3f4-a5b6-7890-1234-56789abcde03"), ".jpeg", "nandos.jpeg", "https://localhost:7065/Images/RestaurantIcon/nandos.jpeg", 361342L, 2, null, null, new DateTime(2026, 1, 9, 21, 55, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("c2d3e4f5-a6b7-7890-1234-56789abcde09"), ".jpeg", "macdonalds.jpeg", "https://localhost:7065/Images/RestaurantIcon/macdonalds.jpeg", 267194L, 2, null, null, new DateTime(2026, 1, 9, 22, 25, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("d1e2f3a4-b5c6-7890-1234-56789abcde04"), ".jpeg", "pedros.jpeg", "https://localhost:7065/Images/RestaurantIcon/pedros.jpeg", 373373L, 2, null, null, new DateTime(2026, 1, 9, 22, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("d2e3f4a5-b6c7-7890-1234-56789abcde10"), ".jpeg", "burger_king.jpeg", "https://localhost:7065/Images/RestaurantIcon/burger_king.jpeg", 371497L, 2, null, null, new DateTime(2026, 1, 9, 22, 30, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("e1f2a3b4-c5d6-7890-1234-56789abcde05"), ".jpeg", "steers.jpeg", "https://localhost:7065/Images/RestaurantIcon/steers.jpeg", 388298L, 2, null, null, new DateTime(2026, 1, 9, 22, 5, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("f1a2b3c4-d5e6-7890-1234-56789abcde06"), ".jpeg", "spur.jpeg", "https://localhost:7065/Images/RestaurantIcon/spur.jpeg", 359922L, 2, null, null, new DateTime(2026, 1, 9, 22, 10, 0, 0, DateTimeKind.Unspecified), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-1234-56789abcde01"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("a2b3c4d5-e6f7-7890-1234-56789abcde07"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("b1c2d3e4-f5a6-7890-1234-56789abcde02"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-7890-1234-56789abcde08"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("c1d2e3f4-a5b6-7890-1234-56789abcde03"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("c2d3e4f5-a6b7-7890-1234-56789abcde09"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("d1e2f3a4-b5c6-7890-1234-56789abcde04"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("d2e3f4a5-b6c7-7890-1234-56789abcde10"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("e1f2a3b4-c5d6-7890-1234-56789abcde05"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-d5e6-7890-1234-56789abcde06"));
        }
    }
}
