using AutoMapper;
using Catmash.Application.DTOs;
using Catmash.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catmash.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private readonly ICatRepository _catRepository;
        private readonly IMapper _mapper;

        public LeaderboardController(ICatRepository catRepository, IMapper mapper)
        {
            _catRepository = catRepository;
            _mapper = mapper;
        }

        [HttpGet("top/{count:int}")]
        public async Task<ActionResult<List<CatScoreDto>>> GetTopCats(int count = 10)
        {
            var cats = await _catRepository.GetTopCatsAsync(count);
            var result = _mapper.Map<List<CatScoreDto>>(cats);
            return Ok(result);
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
