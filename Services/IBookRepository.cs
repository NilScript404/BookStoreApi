using BookStore.Data;
using BookStore.Models;

namespace BookStore.BookRepositoryService
{
	public interface IBookRepository 
	{
		Task<IEnumerable<Book>> GetBooksAsync();
		Task<Book> GetBookAsync(string title , decimal version);
		Task AddBookAsync(Book book);
		Task UpdateBookAsync(Book book);
		Task DeleteBookAsync(Book book);

	}
}