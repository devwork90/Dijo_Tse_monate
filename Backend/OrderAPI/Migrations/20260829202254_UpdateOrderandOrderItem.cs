using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderandOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalePrice",
                table: "OrderItem",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "OrderItem",
                newName: "ItemName");

            migrationBuilder.AddColumn<int>(
                name: "TotalItems",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalItems",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "OrderItem",
                newName: "TotalePrice");

            migrationBuilder.RenameColumn(
                name: "ItemName",
                table: "OrderItem",
                newName: "ProductName");
        }
    }
}
