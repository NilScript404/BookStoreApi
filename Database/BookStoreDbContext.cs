using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Data
{
	public class BookStoreDbContext : DbContext
	{
		public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }
		
		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<Genre> Genres { get; set; }
		
		
		// public DbSet<BookAuthor> BookAuthors { get; set; }
		// public DbSet<BookGenre> BookGenres { get; set; }
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			/*modelBuilder.Entity<BookAuthor>()
				 .HasKey(ba => new { ba.BookId, ba.AuthorId });
			
			modelBuilder.Entity<BookGenre>()
				.HasKey(bg => new { bg.BookId, bg.GenreId });*/			
		
		}
	}

}