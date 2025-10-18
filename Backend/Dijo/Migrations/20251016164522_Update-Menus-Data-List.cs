using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMenusDataList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Description", "Name", "created_at", "is_active", "updated_at" },
                values: new object[,]
                {
                    { new Guid("0c5074ec-73d6-40c1-810e-c5782d318feb"), "All your Indan Menu", "Indian Food", new DateTime(2025, 4, 10, 20, 15, 0, 0, DateTimeKind.Unspecified), true, null },
                    { new Guid("58077521-cfc5-41a3-8414-5a5c8a7d4da3"), "All your Seafood Menu", "Seafood", new DateTime(2025, 3, 10, 20, 15, 0, 0, DateTimeKind.Unspecified), true, null },
                    { new Guid("89b565b4-bf45-46e1-a798-17dd7df1fd94"), "All your Breakfast meals", "Breakfast", new DateTime(2025, 2, 27, 18, 15, 0, 0, DateTimeKind.Unspecified), true, null },
                    { new Guid("ec4c5514-ae37-4b7c-ac36-671a3923f6ea"), "All health menus", "Health food", new DateTime(2025, 2, 15, 14, 30, 0, 0, DateTimeKind.Unspecified), true, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("0c5074ec-73d6-40c1-810e-c5782d318feb"));

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("58077521-cfc5-41a3-8414-5a5c8a7d4da3"));

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("89b565b4-bf45-46e1-a798-17dd7df1fd94"));

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("ec4c5514-ae37-4b7c-ac36-671a3923f6ea"));
        }
    }
}
