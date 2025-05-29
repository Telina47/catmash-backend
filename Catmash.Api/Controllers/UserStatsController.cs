using AutoMapper;
using Catmash.Application.DTOs;
using Catmash.Application.Interfaces;
using Catmash.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Catmash.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStatsController : ControllerBase
    {
        private readonly IUserStatsService _userStatsService;
        private readonly ICatRepository _catRepository;
        private readonly IMapper _mapper;
        public UserStatsController(IUserStatsService userStatsService, ICatRepository catRepository, IMapper mapper)
        {
            _userStatsService = userStatsService;
            _catRepository = catRepository;
            _mapper = mapper;
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

        [HttpGet("top/{count:int}")]
        public async Task<ActionResult<List<CatScoreDto>>> GetTopCats(int count = 10)
        {
            var cats = await _catRepository.GetTopCatsAsync(count);
            var result = _mapper.Map<List<CatScoreDto>>(cats);
            return Ok(result);
        }
    }
}
