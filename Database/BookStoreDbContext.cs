using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookStore.Data
{
	public class BookStoreDbContext : IdentityDbContext<User, Role, string>
	{
		public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }

		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<Genre> Genres { get; set; }
		
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			/* 
			modelBuilder.Entity<Book>()
				.HasOne(book => book.User)
				.WithMany()
				.HasForeignKey(b => b.UserId);
			modelBuilder.Entity<User>()
				.HasMany(u => u.books)
				*/
		}
	}
}

