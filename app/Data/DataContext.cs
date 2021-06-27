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
        public DbSet<Like> Likes { get; set; }
		public DbSet<Footer> Footers { get; set; }

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
				.WithMany(user => user.Followers)
				.HasForeignKey(follower => follower.FollowerId)
				// Use `DeleteBehaviour.Cascade`
                // if You are not using SQL Server
				.OnDelete(DeleteBehavior.NoAction);

			builder
				.Entity<Follower>()
				.HasOne(follower => follower.FollowedUser)
				.WithMany(user => user.Followed)
				.HasForeignKey(follower => follower.FollowedId)
				// Use `DeleteBehaviour.Cascade`
                // if You are not using SQL Server
				.OnDelete(DeleteBehavior.NoAction);

            builder
				.Entity<Like>()
				.HasKey(key => new { key.UserId, key.PostId });

            builder
				.Entity<Like>()
				.HasOne(like => like.User)
				.WithMany(user => user.Likes)
				.HasForeignKey(like => like.UserId)
				// Use `DeleteBehaviour.Cascade`
                // if You are not using SQL Server
				// .OnDelete(DeleteBehavior.ClientCascade); maybe this gonna work
				.OnDelete(DeleteBehavior.NoAction);

			builder
				.Entity<Like>()
				.HasOne(like => like.Post)
				.WithMany(post => post.Likes)
				.HasForeignKey(like => like.PostId)
				// Use `DeleteBehaviour.Cascade`
                // if You are not using SQL Server
				.OnDelete(DeleteBehavior.NoAction);
        }
    }
}
