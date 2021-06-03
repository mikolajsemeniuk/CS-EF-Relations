using System.Threading.Tasks;

namespace app.Interfaces
{
    public interface IFollowerRepository
    {
        Task ToggleFollowerAsync(int followerId, int followedId);
    }
}