using AutoMapper;
using Catmash.Application.DTOs;
using Catmash.Application.Interfaces;
using Catmash.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catmash.Application.Services
{
    public class UserStatsService : IUserStatsService
    {
        private readonly IVoteRepository _voteRepo;
        private readonly ICatRepository _catRepo;
        private readonly IMapper _mapper;

        public UserStatsService(IVoteRepository voteRepo, ICatRepository catRepo, IMapper mapper)
        {
            _voteRepo = voteRepo;
            _catRepo = catRepo;
            _mapper = mapper;
        }

        public async Task<UserVsGlobalStatsDto> GetComparisonAsync(string voterId, TopCount topCount)
        {
            var allCats = await _catRepo.GetTopCatsAsync(100);
            var votes = await _voteRepo.GetVotesByUserAsync(voterId);

            var votedCatIds = votes.Select(v => v.WinnerCatId).Concat(votes.Select(v => v.LoserCatId)).Distinct().ToHashSet();
            var topGlobal = allCats.OrderByDescending(c => c.Score).Take((int)topCount).ToList();

            var userVoteCounts = votedCatIds
                .Select(id => allCats.FirstOrDefault(c => c.Id == id))
                .Where(c => c != null)
                .OrderByDescending(c => c!.Score)
                .Take((int)topCount)
                .ToList();

            var popularButYouDisagree = topGlobal.Where(c => !votedCatIds.Contains(c.Id)).ToList();
            var underratedByOthers = userVoteCounts.Where(c => !topGlobal.Any(g => g.Id == c!.Id)).ToList();

            var agreementRate = topGlobal.Count == 0
                ? 0
                : topGlobal.Count(c => votedCatIds.Contains(c.Id)) / (double)topGlobal.Count;

            return new UserVsGlobalStatsDto
            {
                TopGlobal = _mapper.Map<List<CatScoreDto>>(topGlobal),
                TopUser = _mapper.Map<List<CatScoreDto>>(userVoteCounts!),
                PopularButYouDisagree = _mapper.Map<List<CatScoreDto>>(popularButYouDisagree),
                UnderratedByOthers = _mapper.Map<List<CatScoreDto>>(underratedByOthers),
                AgreementRate = Math.Round(agreementRate * 100, 2),
                TopCount = topCount
            };
        }
    }
}
