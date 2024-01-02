using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class BorrowedBook
    {
        public int Id { get; set; }
        public int? NumberOfBorrowedCopies { get; set; }
        public DateTime? DateOfBorrow {  get; set; }
        
        [ForeignKey("Book")]
        public int? IdBook {  get; set; }
        public Book? Book { get; set; }
        [ForeignKey("User")]
        public string? UserId {  get; set; }
        public User? User { get; set; }
    }
}
