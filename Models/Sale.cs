namespace SmartPharmacySystem.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal Profit { get; set; }

        public DateTime SaleDate { get; set; }
    }
}
