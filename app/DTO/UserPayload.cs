using System;
using System.Collections.Generic;

namespace app.DTO
{
    public class UserPayload
    {
        public int UserId { get; set; }
		public DateTime CreatedAt { get; set; }
		public ICollection<PostPayload> Posts { get; set; } = new List<PostPayload>();
    }
}