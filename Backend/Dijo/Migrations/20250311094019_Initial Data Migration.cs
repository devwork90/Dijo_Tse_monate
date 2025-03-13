using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "restaurants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    logo_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    is_open = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    menuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_restaurants_Menu_menuId",
                        column: x => x.menuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubMenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_available = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    restaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubMenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubMenu_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubMenu_SubMenu_SubMenuId",
                        column: x => x.SubMenuId,
                        principalTable: "SubMenu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubMenu_restaurants_restaurantId",
                        column: x => x.restaurantId,
                        principalTable: "restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Description", "Name", "created_at", "is_active", "updated_at" },
                values: new object[,]
                {
                    { new Guid("83b89851-d594-48a0-b66b-94ba920a0c70"), "All your chicken menus", "Chicken", new DateTime(2025, 2, 15, 14, 30, 0, 0, DateTimeKind.Unspecified), true, null },
                    { new Guid("b7272c86-286d-465c-b993-10e177f6f056"), "All your Piza Menu", "Piza", new DateTime(2025, 3, 10, 20, 15, 0, 0, DateTimeKind.Unspecified), true, null },
                    { new Guid("c5e708e3-46e5-49ff-bb27-a94cf56fa2fe"), "All your shisa nyama grills", "Grilled", new DateTime(2025, 2, 27, 18, 15, 0, 0, DateTimeKind.Unspecified), true, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_restaurants_menuId",
                table: "restaurants",
                column: "menuId");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenu_MenuId",
                table: "SubMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenu_restaurantId",
                table: "SubMenu",
                column: "restaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenu_SubMenuId",
                table: "SubMenu",
                column: "SubMenuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubMenu");

            migrationBuilder.DropTable(
                name: "restaurants");

            migrationBuilder.DropTable(
                name: "Menu");
        }
    }
}
