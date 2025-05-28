using Catmash.Api.DTOs;
using Catmash.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catmash.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Vote([FromBody] VoteRequestDto vote)
        {
            var voterId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (voterId == null) return Unauthorized();

            await _voteService.VoteForCatsAsync(vote.WinnerCatId, vote.LoserCatId, voterId);
            return Ok();
        }
    }

}
