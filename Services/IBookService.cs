using BookStore.Dto;

namespace BookStore.BookService
{
	public interface IBookService 
	{
		Task<IEnumerable<BookDto>> GetBooksAsync();
		Task<BookDto> GetBookAsync(string Title , decimal Version);
		Task<BookDto> CreateBookAsync(BookDto bookDto);
		Task UpdateBookAsync(string Title , decimal Version ,BookDto bookDto);
		Task DeleteBookAsync(string Title , decimal Version);
	}
}