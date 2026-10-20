using SmartPharmacySystem.Models;

namespace SmartPharmacySystem.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext db)
        {
            if (db.Medicines.Any()) return;

            var today = DateTime.Today;
            db.Medicines.AddRange(
                new Medicine { Name = "Paracetamol 500mg", Category = "Painkiller", Stock = 120, ExpiryDate = today.AddMonths(14), CostPrice = 1.50m, Price = 2.50m },
                new Medicine { Name = "Amoxicillin 250mg", Category = "Antibiotic", Stock = 6, ExpiryDate = today.AddMonths(8), CostPrice = 6m, Price = 9m },
                new Medicine { Name = "Cough Syrup", Category = "Syrup", Stock = 40, ExpiryDate = today.AddDays(15), CostPrice = 45m, Price = 70m },
                new Medicine { Name = "Vitamin C Tablets", Category = "Supplement", Stock = 60, ExpiryDate = today.AddMonths(20), CostPrice = 2m, Price = 4m },
                new Medicine { Name = "Old Antacid", Category = "Digestive", Stock = 25, ExpiryDate = today.AddDays(-10), CostPrice = 3m, Price = 5m }
            );
            db.SaveChanges();
        }
    }
}
