using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Wedding_Planner.Models.DTO
{
    public class VendorViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string VendorName { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; } // Optional for display

        [StringLength(100)]
        public string ContactPerson { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Website { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        public string Description { get; set; }

        [Display(Name = "Hourly Rate")]
        [Range(0, double.MaxValue)]
        public decimal? HourlyRate { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        // For dropdowns
        public List<SelectListItem> Categories { get; set; }

        // For vendor services
        public List<VendorServiceViewModel> Services { get; set; }
    }

}
