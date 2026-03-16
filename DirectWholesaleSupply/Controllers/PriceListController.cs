using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DirectWholesaleSupply.Data;
using DirectWholesaleSupply.ViewModels;

namespace DirectWholesaleSupply.Controllers
{
    [Authorize]
    public class PriceListController : Controller
    {
        private readonly AppDbContext _db;
        public PriceListController(AppDbContext db) { _db = db; }

        public async Task<IActionResult> Index()
        {
            var vm = await BuildPriceListViewModel();
            return View(vm);
        }

        public async Task<IActionResult> Print()
        {
            var vm = await BuildPriceListViewModel();
            return View(vm);
        }

        private async Task<PriceListViewModel> BuildPriceListViewModel()
        {
            var phTz = TimeZoneInfo.FindSystemTimeZoneById(
                OperatingSystem.IsWindows() ? "Singapore Standard Time" : "Asia/Manila");
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, phTz);

            var categories = await _db.Categories.OrderBy(c => c.SortOrder).ToListAsync();
            var products = await _db.Products
                .Where(p => p.IsActive)
                .OrderBy(p => p.SortOrder)
                .ToListAsync();

            // Get last two price histories per product for movement detection
            var productIds = products.Select(p => p.ProductID).ToList();
            var histories = await _db.PriceHistories
                .Where(h => productIds.Contains(h.ProductID))
                .OrderByDescending(h => h.ChangedAt)
                .ToListAsync();

            var groups = new List<CategoryProductGroup>();
            foreach (var cat in categories)
            {
                var catProducts = products.Where(p => p.CategoryID == cat.CategoryID).ToList();
                if (!catProducts.Any()) continue;

                var pVms = catProducts.Select(p =>
                {
                    var hist = histories.Where(h => h.ProductID == p.ProductID).ToList();
                    int wMovement = 0, rMovement = 0;
                    if (hist.Count >= 1)
                    {
                        var last = hist[0];
                        wMovement = last.NewWholesalePrice.CompareTo(last.OldWholesalePrice);
                        rMovement = last.NewRetailPrice.CompareTo(last.OldRetailPrice);
                    }
                    return new ProductViewModel
                    {
                        Product = p,
                        WholesalePriceMovement = wMovement,
                        RetailPriceMovement = rMovement
                    };
                }).ToList();

                groups.Add(new CategoryProductGroup { Category = cat, Products = pVms });
            }

            return new PriceListViewModel { Groups = groups, PrintDate = now };
        }
    }

    [Authorize(Roles = "Admin")]
    public class AuditLogsController : Controller
    {
        private readonly AppDbContext _db;
        public AuditLogsController(AppDbContext db) { _db = db; }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 30;
            var phTz = TimeZoneInfo.FindSystemTimeZoneById(
                OperatingSystem.IsWindows() ? "Singapore Standard Time" : "Asia/Manila");

            var total = await _db.AuditLogs.CountAsync();
            var logs = await _db.AuditLogs
                .Include(l => l.User)
                .OrderByDescending(l => l.LogDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            foreach (var log in logs)
                log.LogDate = TimeZoneInfo.ConvertTimeFromUtc(log.LogDate, phTz);

            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)total / pageSize);
            return View(logs);
        }
    }
}
