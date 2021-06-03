using System.Threading.Tasks;

namespace app.Interfaces
{
    public interface ILikeRepository
    {
        Task ToggleLikeAsync(int userId, int postId);
    }
}