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
        [RegularExpression(@"^(?!0{11})\d{11}$", ErrorMessage = "Incorrect Pesel")]
        public string? Pesel { get; set; }
        
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email doesnt match with requirements")]
        public string? Email { get; set; }
       
        [Required]
        public string? Password { get; set; }

        public int? IdAddress { get; set; }
        
        [Required]
        public string? Street { get; set;}
        [Required]
        [RegularExpression(@"\d{2}-\d{3}", ErrorMessage = "Incorrect Postal Code.")]
        public string? PostalCode { get; set;}
        [Required]
        public string? City { get; set;}
    }
}
