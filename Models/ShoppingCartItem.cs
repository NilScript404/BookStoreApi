namespace BookStore.Models
{
	public class ShoppingCartItem
	{
		public int Id { get; set; }
		public int CartId { get; set; }
		public int BookId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		
		public ShoppingCart ShoppingCart { get; set; }
		public Book Book { get; set; }
	}
}
