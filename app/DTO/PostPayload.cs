using System;
using System.Collections.Generic;

namespace app.DTO
{
    public class PostPayload
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public ICollection<LikePayload> Likes { get; set; } = new List<LikePayload>();
        public FooterPayload Footer { get; set; }
    }
}