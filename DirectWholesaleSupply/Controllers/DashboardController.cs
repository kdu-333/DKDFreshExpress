using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DirectWholesaleSupply.Data;
using DirectWholesaleSupply.ViewModels;

namespace DirectWholesaleSupply.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;
        public DashboardController(AppDbContext db) { _db = db; }

        public async Task<IActionResult> Index()
        {
            var phTimeZone = TimeZoneInfo.FindSystemTimeZoneById(
                OperatingSystem.IsWindows() ? "Singapore Standard Time" : "Asia/Manila");
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, phTimeZone);
            var todayStart = now.Date;
            var todayStartUtc = TimeZoneInfo.ConvertTimeToUtc(todayStart, phTimeZone);

            var recentHistory = await _db.PriceHistories
                .Include(h => h.Product).ThenInclude(p => p!.Category)
                .Include(h => h.User)
                .OrderByDescending(h => h.ChangedAt)
                .Take(10)
                .ToListAsync();

            var vm = new DashboardViewModel
            {
                TotalProducts = await _db.Products.CountAsync(),
                TotalCategories = await _db.Categories.CountAsync(),
                ActiveProducts = await _db.Products.CountAsync(p => p.IsActive),
                PriceChangesToday = await _db.PriceHistories.CountAsync(h => h.ChangedAt >= todayStartUtc),
                RecentPriceUpdates = recentHistory.Select(h => new PriceHistoryViewModel
                {
                    ProductName = h.Product?.ProductName ?? "",
                    CategoryName = h.Product?.Category?.CategoryName ?? "",
                    OldWholesalePrice = h.OldWholesalePrice,
                    NewWholesalePrice = h.NewWholesalePrice,
                    OldRetailPrice = h.OldRetailPrice,
                    NewRetailPrice = h.NewRetailPrice,
                    UserFullName = h.User?.FullName ?? "",
                    ChangedAt = TimeZoneInfo.ConvertTimeFromUtc(h.ChangedAt, phTimeZone)
                }).ToList()
            };

            return View(vm);
        }
    }
}
