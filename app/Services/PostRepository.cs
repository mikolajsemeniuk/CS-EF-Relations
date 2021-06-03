using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Data;
using app.DTO;
using app.Interfaces;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Services
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;

        public PostRepository(DataContext context)
        {
            _context = context;
        }
        
        public async Task<PostPayload> AddPostAsync(int userId, string title)
        {
            if (await _context.Users.FindAsync(userId) == null)
                throw new Exception("there is no user with this id");
            
            var post = new Post
            {
                Title = title,
                UserId = userId
            };
            _context.Posts.Add(post);
            if (await _context.SaveChangesAsync() < 1)
                throw new Exception("something went wrong");

            return new PostPayload
            {
                PostId = post.PostId,
                Title = post.Title,
                CreatedAt = post.CreatedAt,
                UserId = post.UserId
            };
        }

        public async Task<PostPayload> GetUserPostAsync(int userId, int postId)
        {
            return await _context.Posts
                .Where(post => post.UserId == userId && post.PostId == postId)
                .Select(post => new PostPayload
                {
                    PostId = post.PostId,
                    Title = post.Title,
                    CreatedAt = post.CreatedAt,
                    UserId = post.UserId
                }).SingleAsync();
        }

        public async Task<IEnumerable<PostPayload>> GetUserPostsAsync(int userId)
        {
            return await _context.Posts
                .Where(post => post.UserId == userId)
                .Select(post => new PostPayload
                {
                    PostId = post.PostId,
                    Title = post.Title,
                    CreatedAt = post.CreatedAt,
                    UserId = post.UserId
                }).ToListAsync();
        }

        public async Task RemovePostAsync(int userId, int postId)
        {
            if (await _context.Users.FindAsync(userId) == null)
                throw new Exception("there is no user with this id");

            var post = await _context.Posts.FindAsync(postId);
            _context.Remove(post);

            if (await _context.SaveChangesAsync() < 1)
                throw new Exception("something went wrong");
        }

        public async Task<PostPayload> UpdatePostAsync(int userId, int postId, string title)
        {
            if (await _context.Users.FindAsync(userId) == null)
                throw new Exception("there is no user with this id");

            var post = await _context.Posts.FindAsync(postId);
            post.Title = title;
            
            if (await _context.SaveChangesAsync() < 1)
                throw new Exception("something went wrong");
            
            return new PostPayload
            {
                PostId = post.PostId,
                Title = post.Title,
                CreatedAt = post.CreatedAt,
                UserId = post.UserId
            };
        }
    }
}