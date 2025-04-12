using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
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
                name: "Restaurants",
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
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuRestaurants",
                columns: table => new
                {
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    restaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuRestaurants", x => new { x.MenuId, x.restaurantId });
                    table.ForeignKey(
                        name: "FK_MenuRestaurants_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuRestaurants_Restaurants_restaurantId",
                        column: x => x.restaurantId,
                        principalTable: "Restaurants",
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
                    restaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubMenu_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubMenu_Restaurants_restaurantId",
                        column: x => x.restaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    is_Available = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubMenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    restaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_Restaurants_restaurantId",
                        column: x => x.restaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItems_SubMenu_SubMenuId",
                        column: x => x.SubMenuId,
                        principalTable: "SubMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Description", "Name", "created_at", "is_active", "updated_at" },
                values: new object[,]
                {
                    { new Guid("769ad0e9-11b4-4c4d-955f-1efa80c04b9b"), "All your Burger Menu", "Burger", new DateTime(2025, 4, 10, 20, 15, 0, 0, DateTimeKind.Unspecified), true, null },
                    { new Guid("83b89851-d594-48a0-b66b-94ba920a0c70"), "All your chicken menus", "Chicken", new DateTime(2025, 2, 15, 14, 30, 0, 0, DateTimeKind.Unspecified), true, null },
                    { new Guid("b7272c86-286d-465c-b993-10e177f6f056"), "All your Piza Menu", "Pizza", new DateTime(2025, 3, 10, 20, 15, 0, 0, DateTimeKind.Unspecified), true, null },
                    { new Guid("c5e708e3-46e5-49ff-bb27-a94cf56fa2fe"), "All your shisa nyama grills", "Grilled", new DateTime(2025, 2, 27, 18, 15, 0, 0, DateTimeKind.Unspecified), true, null }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "created_at", "description", "is_open", "logo_url", "name", "rating", "updated_at" },
                values: new object[,]
                {
                    { new Guid("02d2951c-05a7-4768-b1bb-31891eb522ce"), "07 Soneike", new DateTime(2025, 4, 10, 20, 15, 0, 0, DateTimeKind.Unspecified), "Pizza, Pasta, Salad, Dessert, Mediterranean, Fruit, Fast Food, American, Non-Alcoholic", false, "Romans.jpg", "Roman's Pizza Soneike", 3, null },
                    { new Guid("0dc1443d-9e35-4940-810c-426f55e04ac7"), "04 Soneike ", new DateTime(2025, 4, 10, 20, 15, 0, 0, DateTimeKind.Unspecified), "Chicken, Chicken Wings, Burgers, Wraps, Fast Food, Dessert, American, Spicy, Light Meals, Milkshake", false, "kfc.jpg", "KFC Soneike", 3, null },
                    { new Guid("12b88f56-cc9a-4189-810e-6a1510f6045b"), "07 Stikland", new DateTime(2025, 4, 10, 20, 15, 0, 0, DateTimeKind.Unspecified), "Burgers, Chicken, Fries, Salad, Dessert, Kids, Plant-Based, Healthy, Ribs, Milkshake", false, "Steers.jpg", "Steers Total Stikland", 3, null },
                    { new Guid("1ad4de6c-f138-41d1-af4e-1d0d58e34b80"), "07 Vanreibeek Kuilsriver ", new DateTime(2025, 4, 10, 20, 15, 0, 0, DateTimeKind.Unspecified), "Chicken, Burgers, Chicken Wings, Fast Food, Spicy", false, "hungryLion.jpg", "Hungry Lion Kuils River", 3, null },
                    { new Guid("6d807253-5081-4928-8cea-ba0019f457a7"), "22 Kuilsriver Reno", new DateTime(2025, 4, 10, 20, 15, 0, 0, DateTimeKind.Unspecified), "Steakhouse, Grill, Burgers, Ribs, Chicken, Seafood, Chicken Wings, Dessert, Milkshake, Salad", false, "spur.jpg", "Spur Reno", 3, null },
                    { new Guid("93ec51ab-0862-47fe-a2be-b663c0d36971"), "101 Voortreker Kuilsriver", new DateTime(2025, 4, 10, 20, 15, 0, 0, DateTimeKind.Unspecified), "Chicken, Burgers, Chicken Wings, Fast Food, Light Meals, Portuguese, Salad, Spicy, Dessert, South African", false, "nandos.jpg", "Kuilsriver's Nandos", 3, null },
                    { new Guid("94053c61-d5f8-4d82-a0e8-c6fe2f1d5843"), "07 Haasendal", new DateTime(2025, 4, 10, 20, 15, 0, 0, DateTimeKind.Unspecified), "American, Breakfast, Burgers, Wraps, Cafe, Dessert, Fast Food, Milkshake, Plant-Based, Salad", false, "McDonald .jpg", "McDonald's Haasendal", 3, null },
                    { new Guid("9a9d2d84-e48f-4ff8-b2fa-c45b05421c82"), "102 Zevenwacht ", new DateTime(2025, 4, 10, 20, 15, 0, 0, DateTimeKind.Unspecified), "Burgers, Chicken, Portuguese, Salad, Wraps, Kids, Bowls", false, "pedros.jpg", "Pedros Zevenwacht", 3, null },
                    { new Guid("caf21dbb-ade5-4f51-8f12-ef9862022ba9"), "07 Zevenwacht", new DateTime(2025, 4, 10, 20, 15, 0, 0, DateTimeKind.Unspecified), "Burgers, Chicken Wings, American, Fast Food, Plant-Based, Milkshake, Chicken", false, "BurgerKing.jpg", "Burger King Zevenwacht (Drive-thru)", 3, null },
                    { new Guid("fbfd640e-03b5-4aef-95cf-6112708c4ce5"), "07 Kuilsriver", new DateTime(2025, 4, 10, 20, 15, 0, 0, DateTimeKind.Unspecified), "Pizza, Fast Food, Italian, Chicken Wings, Dessert, Vegetarian", false, "debonairs.jpg", "Debonairs Kuilsriver", 3, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_restaurantId",
                table: "MenuItems",
                column: "restaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_SubMenuId",
                table: "MenuItems",
                column: "SubMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRestaurants_restaurantId",
                table: "MenuRestaurants",
                column: "restaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenu_MenuId",
                table: "SubMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenu_restaurantId",
                table: "SubMenu",
                column: "restaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "MenuRestaurants");

            migrationBuilder.DropTable(
                name: "SubMenu");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
