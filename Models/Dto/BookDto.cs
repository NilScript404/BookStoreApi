
namespace BookStore.Dto
{
	public class BookDTO
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime PublicationDate { get; set; }
		public decimal Price { get; set; }
		public int Rating { get; set; }
		public List<AuthorDTO> Authors { get; set; }
	}
}