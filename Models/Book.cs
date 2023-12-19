using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Book
    {   
        [Key]
        public int Id { get; set; }
        public string? ISBN { get; set; }
        public string? Title { get; set; }
        public string? Description {  get; set; }
        //Photo 

        [ForeignKey("Language")]
        public int? IdLanguage {  get; set; }
        
        [ForeignKey("Publisher")]
        public int? IdPublisher {  get; set; }
        public Publisher? Publisher { get; set; }
        //Expiring time 
    }
}
