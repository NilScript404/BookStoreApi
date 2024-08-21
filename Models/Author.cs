using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStore.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Bio { get; set; }
        public DateTime? DateOfBirth { get; set; }
        
        public ICollection<BookAuthor>? BookAuthors { get; set; }
    }
}