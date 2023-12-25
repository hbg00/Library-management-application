using BookStore.Models;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        [RegularExpression(@"^\d{11}", ErrorMessage = "")]
        public string? Pesel { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Password doesnt match with requirements")]
        public string? Email { get; set; }
        [Required]
        [RegularExpression(@"", ErrorMessage ="")]
        public string? Password { get; set; }
        public int? IdAddress { get; set; }
        [Required]
        public Address? Address { get; set; }
    }
}
