using System.Threading.Tasks;
using app.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeRepository _repository;

        public LikeController(ILikeRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("{userId}/{postId}")]
        public async Task<ActionResult> AddLike(int userId, int postId)
        {
            await _repository.ToggleLikeAsync(userId, postId);
            return Ok();
        }
    }
}