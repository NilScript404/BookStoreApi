namespace BookStore.Models
{
	public class Order
	{
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal TotalAmount { get; set; }
		public string OrderStatus { get; set; }
		
		public Customer Customer { get; set; }
		public ICollection<OrderItem> OrderItems { get; set; }
	}
}
