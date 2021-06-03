using System.Collections.Generic;
using System.Threading.Tasks;
using app.DTO;
using app.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _repository;

        public PostController(IPostRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<PostPayload>>> GetPostsAsync([FromRoute] int userId) =>
            Ok(await _repository.GetUserPostsAsync(userId));

        [HttpGet("{userId}/{id}")]
        public async Task<ActionResult<PostPayload>> GetPostAsync([FromRoute] int userId, [FromRoute] int id) =>
            Ok(await _repository.GetUserPostAsync(userId, id));

        [HttpPost("{userId}/{title}")]
        public async Task<ActionResult<PostPayload>> AddPostAsync([FromRoute] int userId, [FromRoute] string title) =>
            Ok(await _repository.AddPostAsync(userId, title));

        [HttpDelete("{userId}/{id}")]
        public async Task<ActionResult> RemovePostAsync([FromRoute] int userId, [FromRoute] int id)
        {
            await _repository.RemovePostAsync(userId, id);
            return NoContent();
        }

        [HttpPut("{userId}/{id}/{title}")]
        public async Task<ActionResult> RemoveUserAsync([FromRoute] int userId, [FromRoute] int id, [FromRoute] string title) =>
            Ok(await _repository.UpdatePostAsync(userId, id, title));
            
    }
}