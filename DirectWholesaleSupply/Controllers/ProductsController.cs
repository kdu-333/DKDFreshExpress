using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using DirectWholesaleSupply.Data;
using DirectWholesaleSupply.Models;
using DirectWholesaleSupply.ViewModels;

namespace DirectWholesaleSupply.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _db;
        public ProductsController(AppDbContext db) { _db = db; }

        private TimeZoneInfo PhTimeZone => TimeZoneInfo.FindSystemTimeZoneById(
            OperatingSystem.IsWindows() ? "Singapore Standard Time" : "Asia/Manila");

        private int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<IActionResult> Index(int? categoryId, string? search)
        {
            var query = _db.Products.Include(p => p.Category).AsQueryable();
            if (categoryId.HasValue) query = query.Where(p => p.CategoryID == categoryId.Value);
            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.ProductName.Contains(search));

            var products = await query.OrderBy(p => p.Category!.SortOrder).ThenBy(p => p.SortOrder).ToListAsync();
            ViewBag.Categories = await _db.Categories.OrderBy(c => c.SortOrder).ToListAsync();
            ViewBag.SelectedCategory = categoryId;
            ViewBag.Search = search;
            return View(products);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _db.Categories.OrderBy(c => c.SortOrder).ToListAsync();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Product model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _db.Categories.OrderBy(c => c.SortOrder).ToListAsync();
                return View(model);
            }
            _db.Products.Add(model);
            await _db.SaveChangesAsync();
            await WriteAuditLog("Add Product", $"Added product: {model.ProductName}");
            TempData["Success"] = "Product added successfully.";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();
            ViewBag.Categories = await _db.Categories.OrderBy(c => c.SortOrder).ToListAsync();
            return View(product);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Product model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _db.Categories.OrderBy(c => c.SortOrder).ToListAsync();
                return View(model);
            }
            _db.Products.Update(model);
            await _db.SaveChangesAsync();
            await WriteAuditLog("Edit Product", $"Edited product: {model.ProductName}");
            TempData["Success"] = "Product updated successfully.";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductID == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                await WriteAuditLog("Delete Product", $"Deleted product: {product.ProductName}");
                TempData["Success"] = "Product deleted.";
            }
            return RedirectToAction("Index");
        }

        // ── Price Update ──────────────────────────────────────────────────────────

        [HttpGet, Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> UpdatePrices(int? categoryId)
        {
            var query = _db.Products.Include(p => p.Category)
                .Where(p => p.IsActive).AsQueryable();
            if (categoryId.HasValue) query = query.Where(p => p.CategoryID == categoryId.Value);

            var products = await query
                .OrderBy(p => p.Category!.SortOrder).ThenBy(p => p.SortOrder).ToListAsync();
            ViewBag.Categories = await _db.Categories.OrderBy(c => c.SortOrder).ToListAsync();
            ViewBag.SelectedCategory = categoryId;
            return View(products);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> SavePrices(List<UpdatePriceViewModel> prices)
        {
            var userId = CurrentUserId;
            var now = DateTime.UtcNow;
            int count = 0;

            foreach (var p in prices)
            {
                var product = await _db.Products.FindAsync(p.ProductID);
                if (product == null) continue;

                bool changed = product.WholesalePrice != p.WholesalePrice || product.RetailPrice != p.RetailPrice;
                if (!changed) continue;

                _db.PriceHistories.Add(new PriceHistory
                {
                    ProductID = product.ProductID,
                    UserID = userId,
                    OldWholesalePrice = product.WholesalePrice,
                    NewWholesalePrice = p.WholesalePrice,
                    OldRetailPrice = product.RetailPrice,
                    NewRetailPrice = p.RetailPrice,
                    ChangedAt = now
                });

                product.WholesalePrice = p.WholesalePrice;
                product.RetailPrice = p.RetailPrice;
                count++;
            }

            await _db.SaveChangesAsync();
            if (count > 0)
                await WriteAuditLog("Update Prices", $"Updated prices for {count} product(s).");

            TempData["Success"] = $"{count} price(s) updated.";
            return RedirectToAction("UpdatePrices");
        }

        // ── Quick Adjust ──────────────────────────────────────────────────────────

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> QuickAdjust(QuickAdjustViewModel model)
        {
            var product = await _db.Products.FindAsync(model.ProductID);
            if (product == null) return NotFound();

            var oldW = product.WholesalePrice;
            var oldR = product.RetailPrice;

            if (model.Field == "wholesale" || model.Field == "both")
                product.WholesalePrice = Math.Max(0, product.WholesalePrice + model.Amount);
            if (model.Field == "retail" || model.Field == "both")
                product.RetailPrice = Math.Max(0, product.RetailPrice + model.Amount);

            _db.PriceHistories.Add(new PriceHistory
            {
                ProductID = product.ProductID,
                UserID = CurrentUserId,
                OldWholesalePrice = oldW,
                NewWholesalePrice = product.WholesalePrice,
                OldRetailPrice = oldR,
                NewRetailPrice = product.RetailPrice,
                ChangedAt = DateTime.UtcNow
            });

            await _db.SaveChangesAsync();
            await WriteAuditLog("Quick Adjust", $"{product.ProductName}: ₱{model.Amount:+0.##;-0.##} on {model.Field}");

            return Json(new
            {
                success = true,
                wholesale = product.WholesalePrice,
                retail = product.RetailPrice,
                wholesaleMovement = product.WholesalePrice.CompareTo(oldW),
                retailMovement = product.RetailPrice.CompareTo(oldR)
            });
        }

        // ── Helpers ───────────────────────────────────────────────────────────────

        private async Task WriteAuditLog(string action, string details)
        {
            _db.AuditLogs.Add(new AuditLog
            {
                UserID = CurrentUserId,
                Action = action,
                Details = details,
                LogDate = DateTime.UtcNow
            });
            await _db.SaveChangesAsync();
        }
    }
}
