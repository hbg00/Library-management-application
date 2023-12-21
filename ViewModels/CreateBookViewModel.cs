using BookStore.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using BookStore.Data.Enum;

namespace BookStore.ViewModels
{
    public class CreateBookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage="ISBN is required")]
        [RegularExpression(@"\d{9,11}", ErrorMessage ="Incorrect value of ISBN")]
        public string ISBN { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(1, int.MaxValue,ErrorMessage ="Incorrect value of Copies")]
        public int NumberOfCopies { get; set; }

        public Languages Language { get; set; }
        public Publisher Publisher { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Incorrect value")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
    }
}
