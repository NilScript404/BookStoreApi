namespace BookStore.Dto
{
	public class BookDto
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime PublicationDate { get; set; }
		public decimal Price { get; set; }
		public int Rating { get; set; }
	
		public List<BookAuthorDto> AuthorDtos { get; set; }
		public List<BookGenreDto> GenreDtos { get; set; }
	
	}
	
	public class BookAuthorDto 
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
	
	}
	
	public class BookGenreDto
	{
		public string Name { get; set; }
	}

}
