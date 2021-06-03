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
        public DbSet<Follower> Followers { get; set; }

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

            builder
				.Entity<Follower>()
				.HasKey(key => new { key.FollowerId, key.FollowedId });

			builder
				.Entity<Follower>()
				.HasOne(follower => follower.FollowerUser)
				.WithMany(follower => follower.Followers)
				.HasForeignKey(follower => follower.FollowerId)
				// Use `DeleteBehaviour.Cascade`
                // if You are not using SQL Server
				.OnDelete(DeleteBehavior.NoAction);

			builder
				.Entity<Follower>()
				.HasOne(follower => follower.FollowedUser)
				.WithMany(follower => follower.Followed)
				.HasForeignKey(follower => follower.FollowedId)
				// Use `DeleteBehaviour.Cascade`
                // if You are not using SQL Server
				.OnDelete(DeleteBehavior.NoAction);
        }
    }
}