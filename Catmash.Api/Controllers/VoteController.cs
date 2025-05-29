using AutoMapper;
using Catmash.Api.DTOs;
using Catmash.Application.DTOs;
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
        private readonly ICatRepository _catRepository;
        private readonly IMapper _mapper;

        public VoteController(IVoteService voteService,ICatRepository catRepository,IMapper mapper)
        {
            _voteService = voteService;
            _catRepository = catRepository;
            _mapper = mapper;
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

        [HttpGet("random")]
        public async Task<ActionResult<List<CatScoreDto>>> GetRandomPair()
        {
            var cats = await _catRepository.GetTwoRandomCatsAsync();
            var result = _mapper.Map<List<CatScoreDto>>(cats);
            return Ok(result);
        }
    }

}
