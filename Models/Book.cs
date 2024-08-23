using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
        
        public List<Author> Authors { get; set; } = [];
        public List<BookAuthor> BookAuthors { get; set; } = [];
        
/*         
        public List<Genre> genres { get; set; } = [];
        public List<BookGenre> bookGenres { get; set; } = [];
*/        
    
    }
}