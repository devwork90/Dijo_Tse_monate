using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddforeignkeyIdtoImagetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageType",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "MenuId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RestaurantId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_MenuId",
                table: "Images",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_RestaurantId",
                table: "Images",
                column: "RestaurantId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Menu_MenuId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Restaurants_RestaurantId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_MenuId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_RestaurantId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageType",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Images");
        }
    }
}
