
namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ISBN { get; set; }
        public int GenreId { get; set; }
        public DateTime PublishedDate { get; set; }
        public int StockQuantity { get; set; }

        public Author Author { get; set; }
        public Genre Genre { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<WishlistItem> WishlistItems { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
