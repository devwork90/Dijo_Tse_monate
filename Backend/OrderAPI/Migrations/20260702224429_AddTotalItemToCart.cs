using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalItemToCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalItems",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalItems",
                table: "Cart");
        }
    }
}
