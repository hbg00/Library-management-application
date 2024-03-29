﻿using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName {  get; set; }
        public string? Pesel {  get; set; }
        public bool CanBorrow { get; set; } = true;
        public ICollection<BorrowedBook> BorrowedBooks { get; set;}

        [ForeignKey("Address")]
        public int? IdAddress {  get; set; }
        public Address? Address { get; set; }
    }
}
