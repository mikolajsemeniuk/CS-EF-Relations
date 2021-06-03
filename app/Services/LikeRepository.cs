using System;
using System.Linq;
using System.Threading.Tasks;
using app.Data;
using app.Interfaces;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Services
{
    public class LikeRepository : ILikeRepository
    {
        private readonly DataContext _context;

        public LikeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task ToggleLikeAsync(int userId, int postId)
        {
            var user = await _context.Users
                .Where(user => user.UserId == userId)
                .Include(user => user.Likes)
                .FirstAsync();
                
            if (user == null)
                throw new Exception("not user with this id");

            var post = await _context.Posts
                .Where(post => post.PostId == postId)
                .Include(post => post.Likes)
                .FirstAsync();

            if (post == null)
                throw new Exception("not post with this id");

            var like = await _context.Likes.FindAsync(userId, postId);

            if (like == null)
            {
                like = new Like
                {
                    UserId = userId,
                    PostId = postId
                };
                user.Likes.Add(like);
            }
            else
            {
                user.Likes.Remove(like);
            }
            if (await _context.SaveChangesAsync() < 1)
                throw new Exception("something gone wrong while saving changes");
        }
    }
}