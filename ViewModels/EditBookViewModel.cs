using BookStore.Data.Enum;
using BookStore.Models;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels
{
    public class EditBookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        [RegularExpression(@"^\d{10}(\d{3})?$", ErrorMessage = "Incorrect value of ISBN")]
        public string ?ISBN { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Incorrect Length of Title.")]
        public string ?Title { get; set; }

        [StringLength(255, ErrorMessage = "Incorrect Length of Title.")]
        public string ?Description { get; set; }

        [Required]
        [Range(1, 200, ErrorMessage = "Incorrect value of Copies")]
        public int ?NumberOfCopies { get; set; }

        public Languages Language { get; set; }

        public int? IdPublisher { get; set; }
    }
}
