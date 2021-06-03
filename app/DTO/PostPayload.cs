using System;

namespace app.DTO
{
    public class PostPayload
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }
}