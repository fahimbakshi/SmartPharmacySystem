using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartPharmacySystem.Data;
using SmartPharmacySystem.Models;

namespace SmartPharmacySystem.Controllers
{
    public class MedicineController : Controller
    {
        private readonly AppDbContext _db;

        public MedicineController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(string search)
        {
            var query = _db.Medicines.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(m => m.Name.ToLower().Contains(s) ||
                                         (m.Category != null && m.Category.ToLower().Contains(s)));
            }

            ViewBag.Search = search;
            return View(await query.OrderBy(m => m.ExpiryDate).ToListAsync());
        }

        public IActionResult Create()
        {
            return View(new Medicine { ExpiryDate = DateTime.Today.AddYears(1) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Medicine medicine)
        {
            if (!ModelState.IsValid) return View(medicine);

            _db.Medicines.Add(medicine);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Medicine added successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var medicine = await _db.Medicines.FindAsync(id);
            if (medicine == null) return NotFound();
            return View(medicine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Medicine medicine)
        {
            if (id != medicine.Id) return NotFound();
            if (!ModelState.IsValid) return View(medicine);

            _db.Update(medicine);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Medicine updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var medicine = await _db.Medicines.FindAsync(id);
            if (medicine != null)
            {
                _db.Medicines.Remove(medicine); // its sales are removed too
                await _db.SaveChangesAsync();
                TempData["Success"] = "Medicine deleted.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
