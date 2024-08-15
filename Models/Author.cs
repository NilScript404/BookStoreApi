using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Author
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        public required string Name { get; set; }

        [StringLength(2000, ErrorMessage = "Biography must be less than 2000 characters")]
        public required string Biography { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public required DateTime DateOfBirth { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}