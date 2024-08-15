using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public required string Title { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [StringLength(500)]
        public required string Description { get; set; }

        [Required]
        [Range(1, 1000.00)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 10)]
        public required string ISBN { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        [Range(0, 1000)]
        public int StockQuantity { get; set; }

        // the author is not referenced by a name
        // but by the id of the author 
        // which is a properpy in the Author Entity
        [ForeignKey(nameof(AuthorId))]
        public required Author Author { get; set; }

        [ForeignKey(nameof(GenreId))]
        public required Genre Genre { get; set; }

    }
}