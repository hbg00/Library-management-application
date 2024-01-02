using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels
{
    public class BorrowBookViewModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Incorrect value of Copies")]
        public int numberOfBorrowedCopies {  get; set; }
    }
}
