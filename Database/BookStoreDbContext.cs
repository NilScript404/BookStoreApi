using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Data
{
	public class BookStoreDbContext : DbContext
	{
		public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
			 : base(options)
		{
		}

		public DbSet<Book> Books { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Genre> Genres { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book>()
				 .HasOne(b => b.Author)
				 .WithMany(a => a.Books)
				 .HasForeignKey(b => b.AuthorId)
				 .OnDelete(DeleteBehavior.ClientSetNull);

			modelBuilder.Entity<Book>()
				 .HasOne(b => b.Genre)
				 .WithMany(g => g.Books)
				 .HasForeignKey(b => b.GenreId)
				 .OnDelete(DeleteBehavior.ClientSetNull);
		}
	}
}