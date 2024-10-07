using BookStore.Dto;
using BookStore.Models;
namespace BookStore.BookService
{
	public interface IBookService
	{
		Task<IEnumerable<BookDto>> GetBooksAsync();
		Task<BookDto> GetBookAsync(string Title, decimal Version);
		Task<BookDto> CreateBookAsync(BookDto bookDto, string userId);
		Task UpdateBookAsync(string Title, decimal Version, BookDto bookDto);
		Task DeleteBookAsync(string Title, decimal Version);
		Task<Book> UserGetBook(string Title, decimal Version);
		Task<IEnumerable<BookDto>> GetSearchedBooks(string genre);
		// for retrieving the list of books a user has uploaded
		Task<IEnumerable<BookDto>> GetUserBooksAsync(string userId);
	}
}