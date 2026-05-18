using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImageSeedingFilePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("08fddb0b-c5f0-4350-93ff-08de5e5f95ae"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("2d089945-d41a-46d4-b165-f4635c078835"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("98e2bd9b-2f01-4e40-aead-0c06a9d266cb"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("b95753b9-8e2f-4d3e-a505-a3ac7aa47214"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("b9c52ec7-517e-4de4-8a38-4151d9d674b0"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("c374d2c3-4cf8-47c0-bc0b-4244296dc7ad"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("dfb0eb59-8ac5-431a-a0a2-b7a5a11ab130"));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("81a98461-31bc-44d3-ac96-fb2947fb1ebe"),
                column: "FileName",
                value: "burger_menu");

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "FileExtension", "FileName", "FilePath", "FileSizeInBytes", "ImageType", "MenuId", "MenuItemId", "RestaurantId", "created_at", "updated_at" },
                values: new object[,]
                {
                    { new Guid("25c28484-1567-4318-32c8-08deb1d91573"), ".gif", "pizza_menu", "/Images/MenuIcon/pizza_menu.gif", 102576L, 1, null, null, null, new DateTime(2026, 1, 28, 21, 25, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("3b117348-f83b-41d9-32ca-08deb1d91573"), ".gif", "breakfast_menu", "/Images/MenuIcon/breakfast_menu.gif", 127884L, 1, null, null, null, new DateTime(2026, 1, 28, 21, 10, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("8dd86d7d-2b17-4628-32cf-08deb1d91573"), ".gif", "grilled_menu", "/Images/MenuIcon/grilled_menu.gif", 83721L, 1, null, null, null, new DateTime(2026, 1, 28, 21, 37, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("92bda4d1-ad25-45ce-32ce-08deb1d91573"), ".gif", "healthy_menu", "/Images/MenuIcon/healthy_menu.gif", 360675L, 1, null, null, null, new DateTime(2026, 1, 28, 21, 40, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("9492d71e-9075-47df-32cb-08deb1d91573"), ".gif", "seafood_menu", "/Images/MenuIcon/seafood_menu.gif", 103380L, 1, null, null, null, new DateTime(2026, 1, 28, 21, 15, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("e0d31b84-3383-4ddf-32cd-08deb1d91573"), ".gif", "indian_menu", "/Images/MenuIcon/indian_menu.gif", 93504L, 1, null, null, null, new DateTime(2026, 1, 28, 21, 20, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("e6446add-b6c5-4cbf-32cc-08deb1d91573"), ".gif", "chicken_menu", "/Images/MenuIcon/chicken_menu.gif", 127856L, 1, null, null, null, new DateTime(2026, 1, 28, 21, 35, 0, 0, DateTimeKind.Unspecified), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("25c28484-1567-4318-32c8-08deb1d91573"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("3b117348-f83b-41d9-32ca-08deb1d91573"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("8dd86d7d-2b17-4628-32cf-08deb1d91573"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("92bda4d1-ad25-45ce-32ce-08deb1d91573"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("9492d71e-9075-47df-32cb-08deb1d91573"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("e0d31b84-3383-4ddf-32cd-08deb1d91573"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("e6446add-b6c5-4cbf-32cc-08deb1d91573"));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("81a98461-31bc-44d3-ac96-fb2947fb1ebe"),
                column: "FileName",
                value: "burger_menu.gif");

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "FileExtension", "FileName", "FilePath", "FileSizeInBytes", "ImageType", "MenuId", "MenuItemId", "RestaurantId", "created_at", "updated_at" },
                values: new object[,]
                {
                    { new Guid("08fddb0b-c5f0-4350-93ff-08de5e5f95ae"), ".gif", "breakfast_menu.gif", "/Images/MenuIcon/breakfast_menu.gif", 127884L, 1, null, null, null, new DateTime(2026, 1, 28, 21, 10, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("2d089945-d41a-46d4-b165-f4635c078835"), ".gif", "grilled_menu.gif", "/Images/MenuIcon/grilled_menu.gif", 83721L, 1, null, null, null, new DateTime(2026, 1, 28, 21, 37, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("98e2bd9b-2f01-4e40-aead-0c06a9d266cb"), ".gif", "seafood_menu.gif", "/Images/MenuIcon/seafood_menu.gif", 103380L, 1, null, null, null, new DateTime(2026, 1, 28, 21, 15, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("b95753b9-8e2f-4d3e-a505-a3ac7aa47214"), ".gif", "healthy_menu.gif", "/Images/MenuIcon/healthy_menu.gif", 360675L, 1, null, null, null, new DateTime(2026, 1, 28, 21, 40, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("b9c52ec7-517e-4de4-8a38-4151d9d674b0"), ".gif", "indian_menu.gif", "/Images/MenuIcon/indian_menu.gif", 93504L, 1, null, null, null, new DateTime(2026, 1, 28, 21, 20, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("c374d2c3-4cf8-47c0-bc0b-4244296dc7ad"), ".gif", "chicken_menu.gif", "/Images/MenuIcon/chicken_menu.gif", 127856L, 1, null, null, null, new DateTime(2026, 1, 28, 21, 35, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("dfb0eb59-8ac5-431a-a0a2-b7a5a11ab130"), ".gif", "pizza_menu.gif", "/Images/MenuIcon/pizza_menu.gif", 102576L, 1, null, null, null, new DateTime(2026, 1, 28, 21, 25, 0, 0, DateTimeKind.Unspecified), null }
                });
        }
    }
}
