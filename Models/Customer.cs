namespace BookStore.Models
{
	public class Customer
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Address { get; set; }
		
		public ICollection<Order> Orders { get; set; }
		public Wishlist Wishlist { get; set; }
		public ShoppingCart ShoppingCart { get; set; }
	}
}
