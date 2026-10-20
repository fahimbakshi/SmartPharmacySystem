using Microsoft.EntityFrameworkCore;
using SmartPharmacySystem.Models;

namespace SmartPharmacySystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }
}
