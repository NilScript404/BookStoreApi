using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Data
{
	public class BookStoreDbContext : DbContext
	{
		public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }
		
		public DbSet<Book> Books { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<BookAuthor> BookAuthors { get; set; }
		
	}
}