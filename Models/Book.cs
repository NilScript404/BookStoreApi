using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        [Range(1500, 2024, ErrorMessage = "Publication date must be value between 1500 and 2024")]
        public int PublicationDate { get; set; }
        
        public decimal Price { get; set; }
        public int Rating { get; set; }
        public int Version { get; set; }
        
        // for validating the user , in order to allow the user to only update or delete
        // his own books
        public string UserId { get; set; }
        public User User { get; set; }
        
        public List<Author> Authors { get; set; } = [];
        public List<Genre> Genres { get; set; } = [];
    
    
    }
}