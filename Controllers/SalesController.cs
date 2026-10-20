using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartPharmacySystem.Data;
using SmartPharmacySystem.Models;

namespace SmartPharmacySystem.Controllers
{
    public class SalesController : Controller
    {
        private readonly AppDbContext _db;

        public SalesController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var sales = await _db.Sales
                .Include(s => s.Medicine)
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();
            return View(sales);
        }

        public async Task<IActionResult> Create()
        {
            await LoadMedicines();
            return View(new Sale { Quantity = 1 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicineId,Quantity")] Sale sale)
        {
            var med = await _db.Medicines.FindAsync(sale.MedicineId);

            if (med == null)
            {
                ModelState.AddModelError("MedicineId", "Please select a medicine.");
            }
            else if (med.ExpiryDate.Date < DateTime.Today)
            {
                ModelState.AddModelError("MedicineId", "This medicine is expired and cannot be sold.");
            }
            else if (sale.Quantity <= 0)
            {
                ModelState.AddModelError("Quantity", "Quantity must be at least 1.");
            }
            else if (sale.Quantity > med.Stock)
            {
                ModelState.AddModelError("Quantity", $"Only {med.Stock} units in stock.");
            }

            if (!ModelState.IsValid)
            {
                await LoadMedicines();
                return View(sale);
            }

            sale.TotalPrice = med.Price * sale.Quantity;
            sale.Profit = (med.Price - med.CostPrice) * sale.Quantity;
            sale.SaleDate = DateTime.Now;
            med.Stock -= sale.Quantity;

            _db.Sales.Add(sale);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Sale recorded and stock updated.";
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadMedicines()
        {
            ViewBag.Medicines = await _db.Medicines
                .Where(m => m.Stock > 0 && m.ExpiryDate >= DateTime.Today)
                .OrderBy(m => m.Name)
                .ToListAsync();
        }
    }
}
