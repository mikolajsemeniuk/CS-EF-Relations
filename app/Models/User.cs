using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace app.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
		public DateTime CreatedAt { get; set; }
		public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Follower> Followers { get; set; } = new List<Follower>();
		public ICollection<Follower> Followed { get; set; } = new List<Follower>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
    }
}