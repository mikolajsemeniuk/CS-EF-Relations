using System.Collections.Generic;
using System.Threading.Tasks;
using app.DTO;

namespace app.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<PostPayload>> GetUserPostsAsync(int userId);
        Task<PostPayload> GetUserPostAsync(int userId, int postId);
        Task<PostPayload> AddPostAsync(int userId, string title);
        Task<PostPayload> UpdatePostAsync(int userId, int postId, string title);
        Task RemovePostAsync(int userId, int postId);
    }
}