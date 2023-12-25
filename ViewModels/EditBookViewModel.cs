using BookStore.Data.Enum;
using BookStore.Models;
using System.ComponentModel.DataAnnotations;
namespace BookStore.ViewModels
{
    public class EditBookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        [RegularExpression(@"\d{9,11}", ErrorMessage = "Incorrect value of ISBN")]
        public string? ISBN { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Incorrect value of Copies")]
        public int? NumberOfCopies { get; set; }

        public Languages Language { get; set; }
        public int? IdPublisher { get; set; }
        public Publisher? Publisher { get; set; }

    }
}
