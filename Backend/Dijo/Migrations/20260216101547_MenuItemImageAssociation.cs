using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class MenuItemImageAssociation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MenuItemIconId",
                table: "MenuItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MenuItemId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("08fddb0b-c5f0-4350-93ff-08de5e5f95ae"),
                column: "MenuItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("2d089945-d41a-46d4-b165-f4635c078835"),
                column: "MenuItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("81a98461-31bc-44d3-ac96-fb2947fb1ebe"),
                column: "MenuItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("98e2bd9b-2f01-4e40-aead-0c06a9d266cb"),
                column: "MenuItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-1234-56789abcde01"),
                column: "MenuItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("a2b3c4d5-e6f7-7890-1234-56789abcde07"),
                column: "MenuItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("b1c2d3e4-f5a6-7890-1234-56789abcde02"),
                column: "MenuItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-7890-1234-56789abcde08"),
                column: "MenuItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("b95753b9-8e2f-4d3e-a505-a3ac7aa47214"),
                column: "MenuItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("b9c52ec7-517e-4de4-8a38-4151d9d674b0"),
                column: "MenuItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("c1d2e3f4-a5b6-7890-1234-56789abcde03"),
                column: "MenuItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("c2d3e4f5-a6b7-7890-1234-56789abcde09"),
                column: "MenuItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("c374d2c3-4cf8-47c0-bc0b-4244296dc7ad"),
                column: "MenuItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("d1e2f3a4-b5c6-7890-1234-56789abcde04"),
                column: "MenuItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("d2e3f4a5-b6c7-7890-1234-56789abcde10"),
                column: "MenuItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("dfb0eb59-8ac5-431a-a0a2-b7a5a11ab130"),
                column: "MenuItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("e1f2a3b4-c5d6-7890-1234-56789abcde05"),
                column: "MenuItemId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-d5e6-7890-1234-56789abcde06"),
                column: "MenuItemId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Images_MenuItemId",
                table: "Images",
                column: "MenuItemId",
                unique: true,
                filter: "[MenuItemId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_MenuItems_MenuItemId",
                table: "Images",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_MenuItems_MenuItemId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_MenuItemId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "MenuItemIconId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "MenuItemId",
                table: "Images");
        }
    }
}
