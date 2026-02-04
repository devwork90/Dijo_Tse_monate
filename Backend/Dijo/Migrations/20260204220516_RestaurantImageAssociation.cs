using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantImageAssociation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Restaurants_RestaurantId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_RestaurantId",
                table: "Images");

            migrationBuilder.AddColumn<Guid>(
                name: "RestaurantIconId",
                table: "Restaurants",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("02d2951c-05a7-4768-b1bb-31891eb522ce"),
                column: "RestaurantIconId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("0dc1443d-9e35-4940-810c-426f55e04ac7"),
                column: "RestaurantIconId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("12b88f56-cc9a-4189-810e-6a1510f6045b"),
                column: "RestaurantIconId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("1ad4de6c-f138-41d1-af4e-1d0d58e34b80"),
                column: "RestaurantIconId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("6d807253-5081-4928-8cea-ba0019f457a7"),
                column: "RestaurantIconId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("93ec51ab-0862-47fe-a2be-b663c0d36971"),
                column: "RestaurantIconId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("94053c61-d5f8-4d82-a0e8-c6fe2f1d5843"),
                column: "RestaurantIconId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("9a9d2d84-e48f-4ff8-b2fa-c45b05421c82"),
                column: "RestaurantIconId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("caf21dbb-ade5-4f51-8f12-ef9862022ba9"),
                column: "RestaurantIconId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("fbfd640e-03b5-4aef-95cf-6112708c4ce5"),
                column: "RestaurantIconId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Images_RestaurantId",
                table: "Images",
                column: "RestaurantId",
                unique: true,
                filter: "[RestaurantId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Restaurants_RestaurantId",
                table: "Images",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Restaurants_RestaurantId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_RestaurantId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "RestaurantIconId",
                table: "Restaurants");

            migrationBuilder.CreateIndex(
                name: "IX_Images_RestaurantId",
                table: "Images",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Restaurants_RestaurantId",
                table: "Images",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id");
        }
    }
}
