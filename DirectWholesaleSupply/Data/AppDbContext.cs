using Microsoft.EntityFrameworkCore;
using DirectWholesaleSupply.Models;

namespace DirectWholesaleSupply.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PriceHistory> PriceHistories { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleID = 1, RoleName = "Admin" },
                new Role { RoleID = 2, RoleName = "Staff" }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, RoleID = 1, FullName = "Administrator", Username = "admin", Password = "admin123", CreatedAt = new DateTime(2025, 1, 1) },
                new User { UserID = 2, RoleID = 2, FullName = "Staff User", Username = "staff", Password = "staff123", CreatedAt = new DateTime(2025, 1, 1) }
            );

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, CategoryName = "Fresh Vegetables", SortOrder = 1 },
                new Category { CategoryID = 2, CategoryName = "Premium Salad Greens", SortOrder = 2 },
                new Category { CategoryID = 3, CategoryName = "Seafood", SortOrder = 3 },
                new Category { CategoryID = 4, CategoryName = "Fresh Fruits", SortOrder = 4 },
                new Category { CategoryID = 5, CategoryName = "Pork – Fattener", SortOrder = 5 },
                new Category { CategoryID = 6, CategoryName = "Pork – Kitsahan / Bagkun", SortOrder = 6 }
            );

            // Seed Products — Fresh Vegetables (Category 1)
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductID = 1, CategoryID = 1, ProductName = "Broccoli", Unit = "kg", WholesalePrice = 60, RetailPrice = 70, SortOrder = 1 },
                new Product { ProductID = 2, CategoryID = 1, ProductName = "Cauliflower", Unit = "kg", WholesalePrice = 55, RetailPrice = 65, SortOrder = 2 },
                new Product { ProductID = 3, CategoryID = 1, ProductName = "Repolyo (Cabbage)", Unit = "kg", WholesalePrice = 35, RetailPrice = 45, SortOrder = 3 },
                new Product { ProductID = 4, CategoryID = 1, ProductName = "Baguio Beans", Unit = "kg", WholesalePrice = 70, RetailPrice = 80, SortOrder = 4 },
                new Product { ProductID = 5, CategoryID = 1, ProductName = "Sitaw (String Beans)", Unit = "kg", WholesalePrice = 50, RetailPrice = 60, SortOrder = 5 },
                new Product { ProductID = 6, CategoryID = 1, ProductName = "Ampalaya (Bitter Gourd)", Unit = "kg", WholesalePrice = 45, RetailPrice = 55, SortOrder = 6 },
                new Product { ProductID = 7, CategoryID = 1, ProductName = "Talong (Eggplant)", Unit = "kg", WholesalePrice = 40, RetailPrice = 50, SortOrder = 7 },
                new Product { ProductID = 8, CategoryID = 1, ProductName = "Kamatis (Tomato)", Unit = "kg", WholesalePrice = 40, RetailPrice = 50, SortOrder = 8 },
                new Product { ProductID = 9, CategoryID = 1, ProductName = "Sibuyas (Onion)", Unit = "kg", WholesalePrice = 60, RetailPrice = 75, SortOrder = 9 },
                new Product { ProductID = 10, CategoryID = 1, ProductName = "Bawang (Garlic)", Unit = "kg", WholesalePrice = 90, RetailPrice = 110, SortOrder = 10 },
                new Product { ProductID = 11, CategoryID = 1, ProductName = "Luya (Ginger)", Unit = "kg", WholesalePrice = 80, RetailPrice = 100, SortOrder = 11 },
                new Product { ProductID = 12, CategoryID = 1, ProductName = "Carrots", Unit = "kg", WholesalePrice = 50, RetailPrice = 60, SortOrder = 12 },
                new Product { ProductID = 13, CategoryID = 1, ProductName = "Patatas (Potato)", Unit = "kg", WholesalePrice = 55, RetailPrice = 65, SortOrder = 13 },
                new Product { ProductID = 14, CategoryID = 1, ProductName = "Sayote (Chayote)", Unit = "kg", WholesalePrice = 30, RetailPrice = 40, SortOrder = 14 },
                new Product { ProductID = 15, CategoryID = 1, ProductName = "Pechay (Bok Choy)", Unit = "kg", WholesalePrice = 35, RetailPrice = 45, SortOrder = 15 },
                new Product { ProductID = 16, CategoryID = 1, ProductName = "Kangkong (Water Spinach)", Unit = "kg", WholesalePrice = 25, RetailPrice = 35, SortOrder = 16 },
                new Product { ProductID = 17, CategoryID = 1, ProductName = "Kulitis (Amaranth Leaves)", Unit = "kg", WholesalePrice = 25, RetailPrice = 35, SortOrder = 17 },
                new Product { ProductID = 18, CategoryID = 1, ProductName = "Okra", Unit = "kg", WholesalePrice = 40, RetailPrice = 50, SortOrder = 18 },
                new Product { ProductID = 19, CategoryID = 1, ProductName = "Squash (Kalabasa)", Unit = "kg", WholesalePrice = 30, RetailPrice = 40, SortOrder = 19 },
                new Product { ProductID = 20, CategoryID = 1, ProductName = "Sweet Pepper (Bell Pepper)", Unit = "kg", WholesalePrice = 80, RetailPrice = 95, SortOrder = 20 },
                new Product { ProductID = 21, CategoryID = 1, ProductName = "Siling Haba (Long Green Chili)", Unit = "kg", WholesalePrice = 60, RetailPrice = 75, SortOrder = 21 },
                new Product { ProductID = 22, CategoryID = 1, ProductName = "Siling Labuyo (Bird's Eye Chili)", Unit = "kg", WholesalePrice = 100, RetailPrice = 120, SortOrder = 22 },
                new Product { ProductID = 23, CategoryID = 1, ProductName = "Upo (Bottle Gourd)", Unit = "pc", WholesalePrice = 25, RetailPrice = 35, SortOrder = 23 },
                new Product { ProductID = 24, CategoryID = 1, ProductName = "Patola (Sponge Gourd)", Unit = "pc", WholesalePrice = 20, RetailPrice = 30, SortOrder = 24 },
                new Product { ProductID = 25, CategoryID = 1, ProductName = "Mais (Corn)", Unit = "pc", WholesalePrice = 15, RetailPrice = 20, SortOrder = 25 },

                // Premium Salad Greens (Category 2)
                new Product { ProductID = 26, CategoryID = 2, ProductName = "Romaine Lettuce", Unit = "kg", WholesalePrice = 120, RetailPrice = 150, SortOrder = 1 },
                new Product { ProductID = 27, CategoryID = 2, ProductName = "Iceberg Lettuce", Unit = "kg", WholesalePrice = 100, RetailPrice = 130, SortOrder = 2 },
                new Product { ProductID = 28, CategoryID = 2, ProductName = "Butter Lettuce", Unit = "kg", WholesalePrice = 130, RetailPrice = 160, SortOrder = 3 },
                new Product { ProductID = 29, CategoryID = 2, ProductName = "Arugula", Unit = "kg", WholesalePrice = 200, RetailPrice = 250, SortOrder = 4 },
                new Product { ProductID = 30, CategoryID = 2, ProductName = "Baby Spinach", Unit = "kg", WholesalePrice = 180, RetailPrice = 220, SortOrder = 5 },
                new Product { ProductID = 31, CategoryID = 2, ProductName = "Kale", Unit = "kg", WholesalePrice = 160, RetailPrice = 200, SortOrder = 6 },
                new Product { ProductID = 32, CategoryID = 2, ProductName = "Mixed Salad Greens", Unit = "kg", WholesalePrice = 150, RetailPrice = 185, SortOrder = 7 },
                new Product { ProductID = 33, CategoryID = 2, ProductName = "Watercress", Unit = "kg", WholesalePrice = 140, RetailPrice = 175, SortOrder = 8 },
                new Product { ProductID = 34, CategoryID = 2, ProductName = "Celery", Unit = "kg", WholesalePrice = 130, RetailPrice = 160, SortOrder = 9 },
                new Product { ProductID = 35, CategoryID = 2, ProductName = "Radicchio", Unit = "kg", WholesalePrice = 180, RetailPrice = 220, SortOrder = 10 },

                // Seafood (Category 3)
                new Product { ProductID = 36, CategoryID = 3, ProductName = "Bangus (Milkfish)", Unit = "kg", WholesalePrice = 160, RetailPrice = 185, SortOrder = 1 },
                new Product { ProductID = 37, CategoryID = 3, ProductName = "Tilapia", Unit = "kg", WholesalePrice = 120, RetailPrice = 145, SortOrder = 2 },
                new Product { ProductID = 38, CategoryID = 3, ProductName = "Galunggong (Blue Mackerel Scad)", Unit = "kg", WholesalePrice = 140, RetailPrice = 165, SortOrder = 3 },
                new Product { ProductID = 39, CategoryID = 3, ProductName = "Alumahan (Indian Mackerel)", Unit = "kg", WholesalePrice = 150, RetailPrice = 175, SortOrder = 4 },
                new Product { ProductID = 40, CategoryID = 3, ProductName = "Tanigue (Spanish Mackerel)", Unit = "kg", WholesalePrice = 280, RetailPrice = 320, SortOrder = 5 },
                new Product { ProductID = 41, CategoryID = 3, ProductName = "Maya-Maya (Red Snapper)", Unit = "kg", WholesalePrice = 300, RetailPrice = 350, SortOrder = 6 },
                new Product { ProductID = 42, CategoryID = 3, ProductName = "Lapu-Lapu (Grouper)", Unit = "kg", WholesalePrice = 350, RetailPrice = 400, SortOrder = 7 },
                new Product { ProductID = 43, CategoryID = 3, ProductName = "Hipon (Shrimp)", Unit = "kg", WholesalePrice = 280, RetailPrice = 320, SortOrder = 8 },
                new Product { ProductID = 44, CategoryID = 3, ProductName = "Pusit (Squid)", Unit = "kg", WholesalePrice = 200, RetailPrice = 240, SortOrder = 9 },
                new Product { ProductID = 45, CategoryID = 3, ProductName = "Talaba (Oyster)", Unit = "kg", WholesalePrice = 120, RetailPrice = 150, SortOrder = 10 },
                new Product { ProductID = 46, CategoryID = 3, ProductName = "Alimango (Mud Crab)", Unit = "kg", WholesalePrice = 450, RetailPrice = 520, SortOrder = 11 },
                new Product { ProductID = 47, CategoryID = 3, ProductName = "Alimasag (Blue Swimming Crab)", Unit = "kg", WholesalePrice = 350, RetailPrice = 400, SortOrder = 12 },
                new Product { ProductID = 48, CategoryID = 3, ProductName = "Dilis (Anchovies)", Unit = "kg", WholesalePrice = 100, RetailPrice = 130, SortOrder = 13 },
                new Product { ProductID = 49, CategoryID = 3, ProductName = "Bisugo (Threadfin Bream)", Unit = "kg", WholesalePrice = 160, RetailPrice = 190, SortOrder = 14 },
                new Product { ProductID = 50, CategoryID = 3, ProductName = "Tuna (Yellowfin)", Unit = "kg", WholesalePrice = 250, RetailPrice = 290, SortOrder = 15 },

                // Fresh Fruits (Category 4)
                new Product { ProductID = 51, CategoryID = 4, ProductName = "Saging (Banana) – Lakatan", Unit = "kg", WholesalePrice = 50, RetailPrice = 65, SortOrder = 1 },
                new Product { ProductID = 52, CategoryID = 4, ProductName = "Saging (Banana) – Latundan", Unit = "kg", WholesalePrice = 40, RetailPrice = 55, SortOrder = 2 },
                new Product { ProductID = 53, CategoryID = 4, ProductName = "Mangga (Mango) – Carabao", Unit = "kg", WholesalePrice = 80, RetailPrice = 100, SortOrder = 3 },
                new Product { ProductID = 54, CategoryID = 4, ProductName = "Mangga (Mango) – Green", Unit = "kg", WholesalePrice = 60, RetailPrice = 75, SortOrder = 4 },
                new Product { ProductID = 55, CategoryID = 4, ProductName = "Pinya (Pineapple)", Unit = "pc", WholesalePrice = 45, RetailPrice = 60, SortOrder = 5 },
                new Product { ProductID = 56, CategoryID = 4, ProductName = "Watermelon", Unit = "kg", WholesalePrice = 25, RetailPrice = 35, SortOrder = 6 },
                new Product { ProductID = 57, CategoryID = 4, ProductName = "Papaya (Ripe)", Unit = "kg", WholesalePrice = 30, RetailPrice = 40, SortOrder = 7 },
                new Product { ProductID = 58, CategoryID = 4, ProductName = "Papaya (Green / Unripe)", Unit = "kg", WholesalePrice = 25, RetailPrice = 35, SortOrder = 8 },
                new Product { ProductID = 59, CategoryID = 4, ProductName = "Kalamansi (Calamondin)", Unit = "kg", WholesalePrice = 60, RetailPrice = 80, SortOrder = 9 },
                new Product { ProductID = 60, CategoryID = 4, ProductName = "Dalandan (Orange)", Unit = "kg", WholesalePrice = 70, RetailPrice = 90, SortOrder = 10 },
                new Product { ProductID = 61, CategoryID = 4, ProductName = "Avocado", Unit = "kg", WholesalePrice = 100, RetailPrice = 130, SortOrder = 11 },
                new Product { ProductID = 62, CategoryID = 4, ProductName = "Santol", Unit = "kg", WholesalePrice = 50, RetailPrice = 65, SortOrder = 12 },
                new Product { ProductID = 63, CategoryID = 4, ProductName = "Lanzones", Unit = "kg", WholesalePrice = 80, RetailPrice = 100, SortOrder = 13 },
                new Product { ProductID = 64, CategoryID = 4, ProductName = "Rambutan", Unit = "kg", WholesalePrice = 60, RetailPrice = 80, SortOrder = 14 },
                new Product { ProductID = 65, CategoryID = 4, ProductName = "Durian", Unit = "kg", WholesalePrice = 100, RetailPrice = 130, SortOrder = 15 },

                // Pork – Fattener (Category 5)
                new Product { ProductID = 66, CategoryID = 5, ProductName = "Pork – Liempo (Belly)", Unit = "kg", WholesalePrice = 280, RetailPrice = 320, SortOrder = 1 },
                new Product { ProductID = 67, CategoryID = 5, ProductName = "Pork – Kasim (Shoulder)", Unit = "kg", WholesalePrice = 250, RetailPrice = 290, SortOrder = 2 },
                new Product { ProductID = 68, CategoryID = 5, ProductName = "Pork – Pigue (Ham / Leg)", Unit = "kg", WholesalePrice = 260, RetailPrice = 300, SortOrder = 3 },
                new Product { ProductID = 69, CategoryID = 5, ProductName = "Pork – Buto (Bones)", Unit = "kg", WholesalePrice = 120, RetailPrice = 150, SortOrder = 4 },
                new Product { ProductID = 70, CategoryID = 5, ProductName = "Pork – Laman (Lean Meat)", Unit = "kg", WholesalePrice = 270, RetailPrice = 310, SortOrder = 5 },
                new Product { ProductID = 71, CategoryID = 5, ProductName = "Pork – Ribs (Spareribs)", Unit = "kg", WholesalePrice = 260, RetailPrice = 300, SortOrder = 6 },
                new Product { ProductID = 72, CategoryID = 5, ProductName = "Pork – Ulo (Head)", Unit = "kg", WholesalePrice = 180, RetailPrice = 220, SortOrder = 7 },
                new Product { ProductID = 73, CategoryID = 5, ProductName = "Pork – Paa (Feet / Pata)", Unit = "kg", WholesalePrice = 200, RetailPrice = 240, SortOrder = 8 },
                new Product { ProductID = 74, CategoryID = 5, ProductName = "Pork – Atay (Liver)", Unit = "kg", WholesalePrice = 160, RetailPrice = 200, SortOrder = 9 },
                new Product { ProductID = 75, CategoryID = 5, ProductName = "Pork – Batok (Nape)", Unit = "kg", WholesalePrice = 250, RetailPrice = 290, SortOrder = 10 },

                // Pork – Kitsahan / Bagkun (Category 6)
                new Product { ProductID = 76, CategoryID = 6, ProductName = "Kitsahan – Regular", Unit = "kg", WholesalePrice = 160, RetailPrice = 200, SortOrder = 1 },
                new Product { ProductID = 77, CategoryID = 6, ProductName = "Kitsahan – Premium", Unit = "kg", WholesalePrice = 190, RetailPrice = 230, SortOrder = 2 },
                new Product { ProductID = 78, CategoryID = 6, ProductName = "Bagkun (Pork Rind / Chicharon Cut)", Unit = "kg", WholesalePrice = 150, RetailPrice = 190, SortOrder = 3 },
                new Product { ProductID = 79, CategoryID = 6, ProductName = "Taba (Fatback)", Unit = "kg", WholesalePrice = 80, RetailPrice = 110, SortOrder = 4 },
                new Product { ProductID = 80, CategoryID = 6, ProductName = "Balunbalunan (Pork Intestines)", Unit = "kg", WholesalePrice = 120, RetailPrice = 160, SortOrder = 5 }
            );
        }
    }
}
