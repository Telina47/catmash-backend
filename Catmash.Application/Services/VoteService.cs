using Catmash.Domain.Interfaces;
using Catmash.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catmash.Application.Services
{

    public class VoteService : IVoteService
    {
        private readonly ICatRepository _catRepo;
        private readonly IVoteRepository _voteRepo;
        private readonly IEloService _eloService;

        public VoteService(ICatRepository catRepo, IVoteRepository voteRepo, IEloService eloService)
        {
            _catRepo = catRepo;
            _voteRepo = voteRepo;
            _eloService = eloService;
        }

        public async Task VoteForCatsAsync(string winnerId, string loserId, string voterId)
        {
            var winner = await _catRepo.GetByIdAsync(winnerId);
            var loser = await _catRepo.GetByIdAsync(loserId);

            if (winner == null || loser == null) throw new InvalidOperationException("Invalid cat IDs");

            _eloService.ApplyElo(winner, loser);

            await _voteRepo.AddAsync(new Vote
            {
                VoterId = voterId,
                WinnerCatId = winnerId,
                LoserCatId = loserId,
                VotedAt = DateTime.UtcNow
            });

            await _catRepo.UpdateAsync(winner);
            await _catRepo.UpdateAsync(loser);
        }
    }
}
