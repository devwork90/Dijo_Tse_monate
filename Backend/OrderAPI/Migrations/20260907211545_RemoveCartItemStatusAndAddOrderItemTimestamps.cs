using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCartItemStatusAndAddOrderItemTimestamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Cart");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "OrderItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "OrderItem");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
