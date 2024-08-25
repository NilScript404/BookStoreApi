using BookStore.Dto;

namespace BookStore.BookService
{
	public interface IBookService 
	{
		Task<IEnumerable<BookDto>> GetBooksAsync();
		Task<BookDto> GetBookAsync(string Title);
		Task<BookDto> CreateBookAsync(BookDto bookDto);
		Task UpdateBookAsync(string Title , BookDto bookDto);
		Task DeleteBookAsync(string Title);
	}
}