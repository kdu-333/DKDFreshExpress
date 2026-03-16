using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using DirectWholesaleSupply.Data;
using DirectWholesaleSupply.Models;

namespace DirectWholesaleSupply.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _db;
        public CategoriesController(AppDbContext db) { _db = db; }

        private int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<IActionResult> Index()
        {
            var cats = await _db.Categories
                .OrderBy(c => c.SortOrder)
                .Select(c => new { c.CategoryID, c.CategoryName, c.SortOrder, Count = c.Products.Count(p => p.IsActive) })
                .ToListAsync();
            return View(cats.Select(c => new { c.CategoryID, c.CategoryName, c.SortOrder, ProductCount = c.Count }));
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category model)
        {
            if (!ModelState.IsValid) return View(model);
            _db.Categories.Add(model);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Category added.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cat = await _db.Categories.FindAsync(id);
            if (cat == null) return NotFound();
            return View(cat);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category model)
        {
            if (!ModelState.IsValid) return View(model);
            _db.Categories.Update(model);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Category updated.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _db.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.CategoryID == id);
            if (cat == null) return NotFound();
            return View(cat);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cat = await _db.Categories.FindAsync(id);
            if (cat != null) { _db.Categories.Remove(cat); await _db.SaveChangesAsync(); }
            TempData["Success"] = "Category deleted.";
            return RedirectToAction("Index");
        }
    }
}
