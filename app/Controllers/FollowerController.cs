using System.Threading.Tasks;
using app.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FollowerController : ControllerBase
    {
        private readonly IFollowerRepository _repository;

        public FollowerController(IFollowerRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("{followerId}/{followedId}")]
        public async Task<ActionResult> AddFollower(int followerId, int followedId)
        {
            await _repository.ToggleFollowerAsync(followerId, followedId);
            return Ok();
        }
    }
}