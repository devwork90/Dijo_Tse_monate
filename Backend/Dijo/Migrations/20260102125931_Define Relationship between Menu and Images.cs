using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class DefineRelationshipbetweenMenuandImages : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "url_menu_icon",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Images",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("0c5074ec-73d6-40c1-810e-c5782d318feb"),
                column: "url_menu_icon",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("58077521-cfc5-41a3-8414-5a5c8a7d4da3"),
                column: "url_menu_icon",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("769ad0e9-11b4-4c4d-955f-1efa80c04b9b"),
                column: "url_menu_icon",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("83b89851-d594-48a0-b66b-94ba920a0c70"),
                column: "url_menu_icon",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("89b565b4-bf45-46e1-a798-17dd7df1fd94"),
                column: "url_menu_icon",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("b7272c86-286d-465c-b993-10e177f6f056"),
                column: "url_menu_icon",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("c5e708e3-46e5-49ff-bb27-a94cf56fa2fe"),
                column: "url_menu_icon",
                value: null);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: new Guid("ec4c5514-ae37-4b7c-ac36-671a3923f6ea"),
                column: "url_menu_icon",
                value: null);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Menu_MenuId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Restaurants_RestaurantId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "url_menu_icon",
                table: "Menu");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Images",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

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
        }
    }
}
