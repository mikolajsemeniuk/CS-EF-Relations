using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
		public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

            builder
				.Entity<User>()
				.HasMany(user => user.Posts)
				.WithOne(post => post.User!) // ! => not nullable
				.HasForeignKey(post => post.UserId)
				.IsRequired();

			builder
				.Entity<Post>()
				.HasOne(post => post.User!) // ! => not nullable
				.WithMany(user => user.Posts)
				.HasForeignKey(post => post.UserId)
				.IsRequired();
        }
    }
}