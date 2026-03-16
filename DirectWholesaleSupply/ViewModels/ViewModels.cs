using DirectWholesaleSupply.Models;

namespace DirectWholesaleSupply.ViewModels
{
    public class LoginViewModel
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class DashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public int PriceChangesToday { get; set; }
        public int ActiveProducts { get; set; }
        public List<PriceHistoryViewModel> RecentPriceUpdates { get; set; } = new();
    }

    public class PriceHistoryViewModel
    {
        public string ProductName { get; set; } = "";
        public string CategoryName { get; set; } = "";
        public decimal OldWholesalePrice { get; set; }
        public decimal NewWholesalePrice { get; set; }
        public decimal OldRetailPrice { get; set; }
        public decimal NewRetailPrice { get; set; }
        public string UserFullName { get; set; } = "";
        public DateTime ChangedAt { get; set; }
    }

    public class ProductViewModel
    {
        public Product Product { get; set; } = new();
        // Price movement vs previous
        public int WholesalePriceMovement { get; set; } = 0; // 1=up, -1=down, 0=same
        public int RetailPriceMovement { get; set; } = 0;
    }

    public class PriceListViewModel
    {
        public List<CategoryProductGroup> Groups { get; set; } = new();
        public DateTime PrintDate { get; set; } = DateTime.Now;
    }

    public class CategoryProductGroup
    {
        public Category Category { get; set; } = new();
        public List<ProductViewModel> Products { get; set; } = new();
    }

    public class UpdatePriceViewModel
    {
        public int ProductID { get; set; }
        public decimal WholesalePrice { get; set; }
        public decimal RetailPrice { get; set; }
    }

    public class QuickAdjustViewModel
    {
        public int ProductID { get; set; }
        public decimal Amount { get; set; }
        public string Field { get; set; } = "both"; // wholesale, retail, both
    }
}
