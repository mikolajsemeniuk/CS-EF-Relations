using System.Collections.Generic;
using System.Threading.Tasks;
using app.DTO;

namespace app.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserPayload>> GetUsersAsync();
        Task<UserPayload> GetUserAsync(int id);
        Task<UserPayload> AddUserAsync();
        Task RemoveUserAsync(int id);
    }
}