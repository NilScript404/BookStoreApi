using BookStore.Data;
using BookStore.Dto;
using BookStore.BookService;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BookRepositoryService
{
	public class BookRepository : IBookRepository 
	{
		private readonly BookStoreDbContext _context;
		
		public BookRepository(BookStoreDbContext context)
		{
			_context = context;
		}
		
		public async Task<IEnumerable<Book>> GetBooksAsync()
		{
			return await _context.Books
				.Include(a => a.Authors)
				.Include(g => g.Genres)
				.ToListAsync();
		}
		
		public async Task<Book> GetBookAsync (string title , decimal version)
		{
			return await _context.Books
				.Include(a => a.Authors)
				.Include(g => g.Genres)
				.FirstOrDefaultAsync(b => b.Title == title && b.Version == version);
		}
	
		public async Task AddBookAsync(Book book)
		{
			_context.Books.Add(book);
			await _context.SaveChangesAsync();
		}
	
		public async Task UpdateBookAsync(Book book)
		{
			_context.Books.Update(book);
			await _context.SaveChangesAsync();
		}
		
		public async Task DeleteBookAsync(Book book)
		{
			_context.Books.Remove(book);
			await _context.SaveChangesAsync();
		}
	}
}