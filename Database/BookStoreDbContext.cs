using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Data
{
	public class BookStoreDbContext : DbContext
	{
		public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
		{
		}
		
		public DbSet<Book> Books { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Genre> Genres { get; set; }

		// using fluentapi for explicit and more organized relationships
		// conventions are cancerous
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book>()
	 .HasOne(b => b.Author)
	 .WithMany(a => a.Books)
	 .HasForeignKey(b => b.AuthorId)
	 .IsRequired(false); // Add this line to make the Author reference optional

			modelBuilder.Entity<Book>()
				 .HasMany(b => b.Genres)
				 .WithMany(g => g.Books)
				 .UsingEntity<BookGenre>(
					  j => j.HasOne(bg => bg.Genre).WithMany(),
					  j => j.HasOne(bg => bg.Book).WithMany(),
					  j =>
					  {
						  j.ToTable("BookGenres");
						  j.HasKey(bg => new { bg.BookId, bg.GenreId });
					  });
		}
	}
	// junction table for many to many relationship between books and genres
	// gotta study more relationships
	public class BookGenre
	{
		public int BookId { get; set; }
		public int GenreId { get; set; }
		public Book Book { get; set; }
		public Genre Genre { get; set; }
	}
}