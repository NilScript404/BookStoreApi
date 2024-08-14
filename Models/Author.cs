using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime DateOfBirth { get; set; }
        
        public ICollection<Book> Books { get; set; }
    }
}
