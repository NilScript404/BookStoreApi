using BookStore.Data;
using BookStore.Dto;
using BookStore.BookService;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
		
		public async Task<Book> GetBookAsync(string title, decimal version)
		{
			return await _context.Books
				.Include(a => a.Authors)
				.Include(g => g.Genres)
				.FirstOrDefaultAsync(b => b.Title == title && b.Version == version);
		}
		
		public async Task<IEnumerable<Book>> GetBookByUserId(string userId)
		{
			return await _context.Books
				.Where(book => book.UserId == userId)
				.Include(book => book.Authors)
				.Include(book => book.Genres)
				.ToListAsync();
		}
		
		public async Task<IEnumerable<Book>> SearchByGenre(string genre)
		{
			var queries = await _context.Books
			.Where(book => book.Genres.Any(g => g.Name == genre))
			.Include(book => book.Authors)
			.Include(book => book.Genres)
			.ToListAsync();
			
			return queries;
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