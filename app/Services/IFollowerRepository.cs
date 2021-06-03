using System;
using System.Threading.Tasks;
using app.Data;
using app.Interfaces;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Services
{
    public class FollowerRepository : IFollowerRepository
    {
        private readonly DataContext _context;

        public FollowerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task ToggleFollowerAsync(int followerId, int followedId)
        {
            var followerUser = await _context.Users
                .Include(user => user.Followers)
                .FirstOrDefaultAsync(user => user.UserId == followerId);

            if (followerUser == null)
                throw new Exception("sourceUser with this id not found");

            var followedUser = await _context.Users
                .Include(user => user.Followed)
                .FirstOrDefaultAsync(user => user.UserId == followedId);

            if (followedUser == null)
                throw new Exception("targetUser with this id not found");

            if (followerId == followedId)
                throw new Exception("You cannot follow yourself");

            var follower = await _context.Followers.FindAsync(followerId, followedId);
            if (follower == null)
            {
                follower = new Follower
                {
                    FollowerId = followerId,
                    FollowedId = followedId
                };
                followerUser.Followers.Add(follower);
            }
            else
            {
                followerUser.Followers.Remove(follower);
            }
            if (await _context.SaveChangesAsync() < 1)
                throw new Exception("error occured while saving changes");
        }
    }
}