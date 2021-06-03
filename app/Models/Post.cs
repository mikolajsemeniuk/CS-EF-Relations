using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace app.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public Footer Footer { get; set; }
    }
}