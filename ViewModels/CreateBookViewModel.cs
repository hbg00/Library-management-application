using BookStore.Models;
using System.ComponentModel.DataAnnotations;
using BookStore.Data.Enum;

namespace BookStore.ViewModels
{
    public class CreateBookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage="ISBN is required")]
        [RegularExpression(@"^\d{10}(\d{3})?$", ErrorMessage = "Incorrect value of ISBN")]
        public string ISBN { get; set; }

        [StringLength(255, ErrorMessage ="Incorrect Length of Title.")]
        public string Title { get; set; }
        
        [StringLength(255, ErrorMessage = "Incorrect Length of Title.")]
        public string Description { get; set; }

        [Required]
        [Range(1, 200,ErrorMessage ="Incorrect value of Copies")]
        public int NumberOfCopies { get; set; }

        public Languages Language { get; set; }
        public Publisher Publisher { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Incorrect value")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
    }
}
