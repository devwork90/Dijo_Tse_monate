using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class InsertaFKMenuIconImageIdinsideMenumodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Menu_MenuId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Restaurants_RestaurantId",
                table: "Images");

            migrationBuilder.AddColumn<Guid>(
                name: "MenuIconImageId",
                table: "Menu",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Images",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("0c5074ec-73d6-40c1-810e-c5782d318feb"),
                column: "MenuIconImageId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("58077521-cfc5-41a3-8414-5a5c8a7d4da3"),
                column: "MenuIconImageId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("769ad0e9-11b4-4c4d-955f-1efa80c04b9b"),
                column: "MenuIconImageId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("83b89851-d594-48a0-b66b-94ba920a0c70"),
                column: "MenuIconImageId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("89b565b4-bf45-46e1-a798-17dd7df1fd94"),
                column: "MenuIconImageId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("b7272c86-286d-465c-b993-10e177f6f056"),
                column: "MenuIconImageId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("c5e708e3-46e5-49ff-bb27-a94cf56fa2fe"),
                column: "MenuIconImageId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("ec4c5514-ae37-4b7c-ac36-671a3923f6ea"),
                column: "MenuIconImageId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Menu_MenuIconImageId",
                table: "Menu",
                column: "MenuIconImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Menu_MenuId",
                table: "Images",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Restaurants_RestaurantId",
                table: "Images",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Images_MenuIconImageId",
                table: "Menu",
                column: "MenuIconImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Menu_MenuId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Restaurants_RestaurantId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Images_MenuIconImageId",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Menu_MenuIconImageId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "MenuIconImageId",
                table: "Menu");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Menu_MenuId",
                table: "Images",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Restaurants_RestaurantId",
                table: "Images",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
