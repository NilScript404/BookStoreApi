namespace BookStore.Dto
{
	public class CreateBookDTO
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime PublicationDate { get; set; }
		public decimal Price { get; set; }
		public int Rating { get; set; }
		public List<CreateAuthorDTO> Authors { get; set; }
	}
}