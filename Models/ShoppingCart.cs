namespace BookStore.Models
{
	public class ShoppingCart
	{
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public DateTime CreatedDate { get; set; }

		public Customer Customer { get; set; }
		public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
	}
}
