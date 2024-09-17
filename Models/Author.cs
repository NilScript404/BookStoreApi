using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStore.Models
{
    public class Author
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public int DateOfBirth { get; set; }
        
        public List<Book> Books { get; set; } = [];
        // public List<BookAuthor> BookAuthors { get; set; } = [];
    
    }
}