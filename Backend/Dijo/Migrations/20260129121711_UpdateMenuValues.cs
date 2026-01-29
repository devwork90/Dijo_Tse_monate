using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMenuValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("b95753b9-8e2f-4d3e-a505-a3ac7aa47214"),
                column: "FilePath",
                value: "https://localhost:7065/Images/MenuIcon/healthy_food_menu.gif");

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("ec4c5514-ae37-4b7c-ac36-671a3923f6ea"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "All healthy menus", "Healthy food" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("b95753b9-8e2f-4d3e-a505-a3ac7aa47214"),
                column: "FilePath",
                value: "https://localhost:7065/Images/MenuIcon/health_food_menu.gif");

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("ec4c5514-ae37-4b7c-ac36-671a3923f6ea"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "All health menus", "Health food" });
        }
    }
}
