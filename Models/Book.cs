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
       
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
        public ICollection<BookAuthor>? BookAuthors { get; set; }
    }
}