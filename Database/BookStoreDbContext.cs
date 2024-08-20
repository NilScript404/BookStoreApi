using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Data
{
	public class BookStoreDbContext : DbContext
	{
		public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }
		
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
		
		protected override void OnModelCreating(ModelBuilder modelbuilder)
		{
			modelbuilder.Entity<BookAuthor>()
				.HasKey(k => new { k.BookId, k.AuthorId });
		}	
	}

}