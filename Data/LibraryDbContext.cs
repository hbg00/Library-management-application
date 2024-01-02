using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookStore.Data
{
    public class LibraryDbContext : IdentityDbContext<User>
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options )
        {

        } 
        public DbSet<BorrowedBook> BorrowedBooks {  get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Address> Addresss { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
    }
}
