using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHealthyMenuFilePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("b95753b9-8e2f-4d3e-a505-a3ac7aa47214"),
                column: "FilePath",
                value: "https://localhost:7065/Images/MenuIcon/healthy_menu.gif");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("b95753b9-8e2f-4d3e-a505-a3ac7aa47214"),
                column: "FilePath",
                value: "https://localhost:7065/Images/MenuIcon/healthy_food_menu.gif");
        }
    }
}
