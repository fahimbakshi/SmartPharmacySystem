using System.ComponentModel.DataAnnotations;

namespace SmartPharmacySystem.Models
{
    public class Medicine
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Medicine name is required")]
        public string Name { get; set; }

        public string Category { get; set; }

        [Range(0, 100000, ErrorMessage = "Stock must be 0 or more")]
        public int Stock { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        [Range(0, 1000000)]
        [Display(Name = "Cost Price")]
        public decimal CostPrice { get; set; }

        [Range(0, 1000000)]
        [Display(Name = "Selling Price")]
        public decimal Price { get; set; }

        public List<Sale> Sales { get; set; }
    }
}
