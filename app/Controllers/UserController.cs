using System.Collections.Generic;
using System.Threading.Tasks;
using app.DTO;
using app.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPayload>>> GetUsersAsync() =>
            Ok(await _repository.GetUsersAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<UserPayload>> GetUserAsync([FromRoute] int id) =>
            Ok(await _repository.GetUserAsync(id));

        [HttpPost]
        public async Task<ActionResult<UserPayload>> AddUserAsync() =>
            Ok(await _repository.AddUserAsync());

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveUserAsync([FromRoute] int id)
        {
            await _repository.RemoveUserAsync(id);
            return NoContent();
        }
    }
}