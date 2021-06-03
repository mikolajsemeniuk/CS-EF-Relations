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
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        
        public async Task<UserPayload> AddUserAsync()
        {
            var user = new User
            {
                CreatedAt = DateTime.Now
            };
            _context.Users.Add(user);
            if (await _context.SaveChangesAsync() < 1)
                throw new Exception("error while saving");
            return new UserPayload
            {
                UserId = user.UserId,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserPayload> GetUserAsync(int id)
        {
            return await _context
                .Users
                .Where(user => user.UserId == id)
                .Include(user => user.Followers)
                .Include(user => user.Followed)
                .Include(user => user.Posts)
                .ThenInclude(post => post.Likes)
                .Select(user => new UserPayload
                {
                    UserId = user.UserId,
                    CreatedAt = user.CreatedAt,
                    Posts = user.Posts.Select(post => new PostPayload
                    {
                        PostId = post.PostId,
                        Title = post.Title,
                        CreatedAt = post.CreatedAt,
                        UserId = post.UserId,
                        Likes = post.Likes.Select(like => new LikePayload
                        {
                            LikerId = like.UserId,
                        }).ToList(),
                        Footer = new FooterPayload
                        {
                            Reference = post.Footer.Reference
                        },
                    }).ToList(),
                    Followers = user.Followers.Select(follower => new FollowerPayload
                    {
                        FollowerId = follower.FollowedId,
                    }).ToList(),
                    Followed = user.Followed.Select(follower => new FollowerPayload
                    {
                        FollowerId = follower.FollowerId
                    }).ToList()
                })
                .AsNoTracking()
                .SingleAsync();
        }

        public async Task<IEnumerable<UserPayload>> GetUsersAsync()
        {
            return await _context
                .Users
                .Include(user => user.Followers)
                .Include(user => user.Followed)
                .Include(user => user.Posts)
                .ThenInclude(post => post.Likes)
                .Select(user => new UserPayload
                {
                    UserId = user.UserId,
                    CreatedAt = user.CreatedAt,
                    Posts = user.Posts.Select(post => new PostPayload
                    {
                        PostId = post.PostId,
                        Title = post.Title,
                        CreatedAt = post.CreatedAt,
                        UserId = post.UserId,
                        Likes = post.Likes.Select(like => new LikePayload
                        {
                            LikerId = like.UserId,
                        }).ToList(),
                        Footer = new FooterPayload
                        {
                            Reference = post.Footer.Reference
                        },
                    }).ToList(),
                    Followers = user.Followers.Select(follower => new FollowerPayload
                    {
                        FollowerId = follower.FollowedId,
                    }).ToList(),
                    Followed = user.Followed.Select(follower => new FollowerPayload
                    {
                        FollowerId = follower.FollowerId
                    }).ToList()
                }).ToListAsync();
        }

        public async Task RemoveUserAsync(int id)
        {
            var likes = await _context.Likes.FindAsync(id);

            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);

            if (await _context.SaveChangesAsync() < 1)
                throw new Exception("error while saving");
        }
    }
}