using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartPharmacySystem.Data;
using SmartPharmacySystem.Models;

namespace SmartPharmacySystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;

        public DashboardController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;
            var soon = today.AddDays(30);

            var meds = await _db.Medicines.ToListAsync();
            var sales = await _db.Sales.ToListAsync();

            var vm = new DashboardViewModel
            {
                TotalMedicines = meds.Count,
                ExpiredList = meds.Where(m => m.ExpiryDate.Date < today).ToList(),
                ExpiringSoonList = meds.Where(m => m.ExpiryDate.Date >= today && m.ExpiryDate.Date <= soon).ToList(),
                LowStockList = meds.Where(m => m.Stock < 10).ToList()
            };

            vm.ExpiredCount = vm.ExpiredList.Count;
            vm.ExpiringSoonCount = vm.ExpiringSoonList.Count;
            vm.LowStockCount = vm.LowStockList.Count;

            vm.SalesToday = sales.Where(s => s.SaleDate.Date == today).Sum(s => s.TotalPrice);
            vm.TotalRevenue = sales.Sum(s => s.TotalPrice);
            vm.TotalProfit = sales.Sum(s => s.Profit);
            vm.ExpiryLoss = vm.ExpiredList.Sum(m => m.Stock * m.CostPrice);
            vm.NetProfit = vm.TotalProfit - vm.ExpiryLoss;

            for (int i = 6; i >= 0; i--)
            {
                var day = today.AddDays(-i);
                vm.ChartLabels.Add(day.ToString("dd MMM"));
                vm.ChartData.Add(sales.Where(s => s.SaleDate.Date == day).Sum(s => s.TotalPrice));
            }

            return View(vm);
        }
    }
}
