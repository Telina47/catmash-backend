using Catmash.Application.DTOs;
using Catmash.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Catmash.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStatsController : ControllerBase
    {
        private readonly IUserStatsService _userStatsService;

        public UserStatsController(IUserStatsService userStatsService)
        {
            _userStatsService = userStatsService;
        }


        [Authorize]
        [HttpGet("comparison/{top:int}")]
        public async Task<ActionResult<UserVsGlobalStatsDto>> GetComparison(int top = 10)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var result = await _userStatsService.GetComparisonAsync(userId,(TopCount) top );
            return Ok(result);
        }
    }
}
