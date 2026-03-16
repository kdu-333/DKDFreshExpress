using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814

namespace DirectWholesaleSupply.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Categories", x => x.CategoryID); });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Roles", x => x.RoleID); });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID   = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    CategoryID  = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Unit            = table.Column<string>(type: "nvarchar(20)",  maxLength: 20,  nullable: false),
                    WholesalePrice  = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    RetailPrice     = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Notes       = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive    = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder   = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey("FK_Products_Categories_CategoryID", x => x.CategoryID,
                        "Categories", "CategoryID", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID    = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    RoleID    = table.Column<int>(type: "int", nullable: false),
                    FullName  = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username  = table.Column<string>(type: "nvarchar(50)",  maxLength: 50,  nullable: false),
                    Password  = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey("FK_Users_Roles_RoleID", x => x.RoleID,
                        "Roles", "RoleID", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    LogID   = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    UserID  = table.Column<int>(type: "int", nullable: false),
                    Action  = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.LogID);
                    table.ForeignKey("FK_AuditLogs_Users_UserID", x => x.UserID,
                        "Users", "UserID", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceHistories",
                columns: table => new
                {
                    HistoryID         = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    ProductID         = table.Column<int>(type: "int", nullable: false),
                    UserID            = table.Column<int>(type: "int", nullable: false),
                    OldWholesalePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    NewWholesalePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    OldRetailPrice    = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    NewRetailPrice    = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ChangedAt         = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceHistories", x => x.HistoryID);
                    table.ForeignKey("FK_PriceHistories_Products_ProductID", x => x.ProductID,
                        "Products", "ProductID", onDelete: ReferentialAction.Cascade);
                    table.ForeignKey("FK_PriceHistories_Users_UserID", x => x.UserID,
                        "Users", "UserID", onDelete: ReferentialAction.NoAction);
                });

            // ── Seed Roles ──
            migrationBuilder.InsertData("Roles",
                new[] { "RoleID", "RoleName" },
                new object[,] { { 1, "Admin" }, { 2, "Staff" } });

            // ── Seed Users ──
            migrationBuilder.InsertData("Users",
                new[] { "UserID", "RoleID", "FullName", "Username", "Password", "CreatedAt" },
                new object[,]
                {
                    { 1, 1, "Administrator", "admin", "admin123", new DateTime(2025, 1, 1) },
                    { 2, 2, "Staff User",    "staff", "staff123", new DateTime(2025, 1, 1) }
                });

            // ── Seed Categories ──
            migrationBuilder.InsertData("Categories",
                new[] { "CategoryID", "CategoryName", "SortOrder" },
                new object[,]
                {
                    { 1, "Fresh Vegetables",        1 },
                    { 2, "Premium Salad Greens",     2 },
                    { 3, "Seafood",                  3 },
                    { 4, "Fresh Fruits",             4 },
                    { 5, "Pork \u2013 Fattener",    5 },
                    { 6, "Pork \u2013 Kitsahan / Bagkun", 6 }
                });

            // ── Seed Products ──
            migrationBuilder.InsertData("Products",
                new[] { "ProductID","CategoryID","ProductName","Unit","WholesalePrice","RetailPrice","Notes","IsActive","SortOrder" },
                new object[,]
                {
                    // Fresh Vegetables (Cat 1)
                    {  1,1,"Broccoli","kg",60m,70m,null,true,1},
                    {  2,1,"Cauliflower","kg",55m,65m,null,true,2},
                    {  3,1,"Repolyo (Cabbage)","kg",35m,45m,null,true,3},
                    {  4,1,"Baguio Beans","kg",70m,80m,null,true,4},
                    {  5,1,"Sitaw (String Beans)","kg",50m,60m,null,true,5},
                    {  6,1,"Ampalaya (Bitter Gourd)","kg",45m,55m,null,true,6},
                    {  7,1,"Talong (Eggplant)","kg",40m,50m,null,true,7},
                    {  8,1,"Kamatis (Tomato)","kg",40m,50m,null,true,8},
                    {  9,1,"Sibuyas (Onion)","kg",60m,75m,null,true,9},
                    { 10,1,"Bawang (Garlic)","kg",90m,110m,null,true,10},
                    { 11,1,"Luya (Ginger)","kg",80m,100m,null,true,11},
                    { 12,1,"Carrots","kg",50m,60m,null,true,12},
                    { 13,1,"Patatas (Potato)","kg",55m,65m,null,true,13},
                    { 14,1,"Sayote (Chayote)","kg",30m,40m,null,true,14},
                    { 15,1,"Pechay (Bok Choy)","kg",35m,45m,null,true,15},
                    { 16,1,"Kangkong (Water Spinach)","kg",25m,35m,null,true,16},
                    { 17,1,"Kulitis (Amaranth Leaves)","kg",25m,35m,null,true,17},
                    { 18,1,"Okra","kg",40m,50m,null,true,18},
                    { 19,1,"Squash (Kalabasa)","kg",30m,40m,null,true,19},
                    { 20,1,"Sweet Pepper (Bell Pepper)","kg",80m,95m,null,true,20},
                    { 21,1,"Siling Haba (Long Green Chili)","kg",60m,75m,null,true,21},
                    { 22,1,"Siling Labuyo (Bird's Eye Chili)","kg",100m,120m,null,true,22},
                    { 23,1,"Upo (Bottle Gourd)","pc",25m,35m,null,true,23},
                    { 24,1,"Patola (Sponge Gourd)","pc",20m,30m,null,true,24},
                    { 25,1,"Mais (Corn)","pc",15m,20m,null,true,25},
                    // Premium Salad Greens (Cat 2)
                    { 26,2,"Romaine Lettuce","kg",120m,150m,null,true,1},
                    { 27,2,"Iceberg Lettuce","kg",100m,130m,null,true,2},
                    { 28,2,"Butter Lettuce","kg",130m,160m,null,true,3},
                    { 29,2,"Arugula","kg",200m,250m,null,true,4},
                    { 30,2,"Baby Spinach","kg",180m,220m,null,true,5},
                    { 31,2,"Kale","kg",160m,200m,null,true,6},
                    { 32,2,"Mixed Salad Greens","kg",150m,185m,null,true,7},
                    { 33,2,"Watercress","kg",140m,175m,null,true,8},
                    { 34,2,"Celery","kg",130m,160m,null,true,9},
                    { 35,2,"Radicchio","kg",180m,220m,null,true,10},
                    // Seafood (Cat 3)
                    { 36,3,"Bangus (Milkfish)","kg",160m,185m,null,true,1},
                    { 37,3,"Tilapia","kg",120m,145m,null,true,2},
                    { 38,3,"Galunggong (Blue Mackerel Scad)","kg",140m,165m,null,true,3},
                    { 39,3,"Alumahan (Indian Mackerel)","kg",150m,175m,null,true,4},
                    { 40,3,"Tanigue (Spanish Mackerel)","kg",280m,320m,null,true,5},
                    { 41,3,"Maya-Maya (Red Snapper)","kg",300m,350m,null,true,6},
                    { 42,3,"Lapu-Lapu (Grouper)","kg",350m,400m,null,true,7},
                    { 43,3,"Hipon (Shrimp)","kg",280m,320m,null,true,8},
                    { 44,3,"Pusit (Squid)","kg",200m,240m,null,true,9},
                    { 45,3,"Talaba (Oyster)","kg",120m,150m,null,true,10},
                    { 46,3,"Alimango (Mud Crab)","kg",450m,520m,null,true,11},
                    { 47,3,"Alimasag (Blue Swimming Crab)","kg",350m,400m,null,true,12},
                    { 48,3,"Dilis (Anchovies)","kg",100m,130m,null,true,13},
                    { 49,3,"Bisugo (Threadfin Bream)","kg",160m,190m,null,true,14},
                    { 50,3,"Tuna (Yellowfin)","kg",250m,290m,null,true,15},
                    // Fresh Fruits (Cat 4)
                    { 51,4,"Saging (Banana) - Lakatan","kg",50m,65m,null,true,1},
                    { 52,4,"Saging (Banana) - Latundan","kg",40m,55m,null,true,2},
                    { 53,4,"Mangga (Mango) - Carabao","kg",80m,100m,null,true,3},
                    { 54,4,"Mangga (Mango) - Green","kg",60m,75m,null,true,4},
                    { 55,4,"Pinya (Pineapple)","pc",45m,60m,null,true,5},
                    { 56,4,"Watermelon","kg",25m,35m,null,true,6},
                    { 57,4,"Papaya (Ripe)","kg",30m,40m,null,true,7},
                    { 58,4,"Papaya (Green / Unripe)","kg",25m,35m,null,true,8},
                    { 59,4,"Kalamansi (Calamondin)","kg",60m,80m,null,true,9},
                    { 60,4,"Dalandan (Orange)","kg",70m,90m,null,true,10},
                    { 61,4,"Avocado","kg",100m,130m,null,true,11},
                    { 62,4,"Santol","kg",50m,65m,null,true,12},
                    { 63,4,"Lanzones","kg",80m,100m,null,true,13},
                    { 64,4,"Rambutan","kg",60m,80m,null,true,14},
                    { 65,4,"Durian","kg",100m,130m,null,true,15},
                    // Pork Fattener (Cat 5)
                    { 66,5,"Pork - Liempo (Belly)","kg",280m,320m,null,true,1},
                    { 67,5,"Pork - Kasim (Shoulder)","kg",250m,290m,null,true,2},
                    { 68,5,"Pork - Pigue (Ham / Leg)","kg",260m,300m,null,true,3},
                    { 69,5,"Pork - Buto (Bones)","kg",120m,150m,null,true,4},
                    { 70,5,"Pork - Laman (Lean Meat)","kg",270m,310m,null,true,5},
                    { 71,5,"Pork - Ribs (Spareribs)","kg",260m,300m,null,true,6},
                    { 72,5,"Pork - Ulo (Head)","kg",180m,220m,null,true,7},
                    { 73,5,"Pork - Paa (Feet / Pata)","kg",200m,240m,null,true,8},
                    { 74,5,"Pork - Atay (Liver)","kg",160m,200m,null,true,9},
                    { 75,5,"Pork - Batok (Nape)","kg",250m,290m,null,true,10},
                    // Pork Kitsahan (Cat 6)
                    { 76,6,"Kitsahan - Regular","kg",160m,200m,null,true,1},
                    { 77,6,"Kitsahan - Premium","kg",190m,230m,null,true,2},
                    { 78,6,"Bagkun (Pork Rind / Chicharon Cut)","kg",150m,190m,null,true,3},
                    { 79,6,"Taba (Fatback)","kg",80m,110m,null,true,4},
                    { 80,6,"Balunbalunan (Pork Intestines)","kg",120m,160m,null,true,5}
                });

            migrationBuilder.CreateIndex("IX_AuditLogs_UserID",   "AuditLogs",    "UserID");
            migrationBuilder.CreateIndex("IX_PriceHistories_ProductID", "PriceHistories", "ProductID");
            migrationBuilder.CreateIndex("IX_PriceHistories_UserID",    "PriceHistories", "UserID");
            migrationBuilder.CreateIndex("IX_Products_CategoryID", "Products", "CategoryID");
            migrationBuilder.CreateIndex("IX_Users_RoleID",        "Users",    "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("AuditLogs");
            migrationBuilder.DropTable("PriceHistories");
            migrationBuilder.DropTable("Products");
            migrationBuilder.DropTable("Users");
            migrationBuilder.DropTable("Categories");
            migrationBuilder.DropTable("Roles");
        }
    }
}
