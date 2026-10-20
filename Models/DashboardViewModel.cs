namespace SmartPharmacySystem.Models
{
    public class DashboardViewModel
    {
        public int TotalMedicines { get; set; }
        public int ExpiredCount { get; set; }
        public int ExpiringSoonCount { get; set; }
        public int LowStockCount { get; set; }

        public decimal SalesToday { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal ExpiryLoss { get; set; }
        public decimal NetProfit { get; set; }

        public List<Medicine> ExpiredList { get; set; } = new();
        public List<Medicine> ExpiringSoonList { get; set; } = new();
        public List<Medicine> LowStockList { get; set; } = new();

        public List<string> ChartLabels { get; set; } = new();
        public List<decimal> ChartData { get; set; } = new();
    }
}
