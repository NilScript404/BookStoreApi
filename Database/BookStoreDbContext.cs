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
		
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		
		}
		
		/* todo ?
		protected override void OnModelCreating(ModelBuilder builder)
   	{
        base.OnModelCreating(builder);
        
        // Customize ASP.NET Identity table names if needed
        builder.Entity<ApplicationUser>(entity => { entity.ToTable("AspNetUsers"); });
        builder.Entity<ApplicationRole>(entity => { entity.ToTable("AspNetRoles"); });
        builder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("AspNetUserClaims"); });
   	}
	*/
	
	}
}

