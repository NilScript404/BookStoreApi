namespace BookStore.Dto
{
	public class UpdateBookDTO
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime PublicationDate { get; set; }
		public decimal Price { get; set; }
		public int Rating { get; set; }
		public List<int> AuthorIds { get; set; }
      public int Id { get; internal set; }
    }
}