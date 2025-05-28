using Catmash.Domain.Interfaces;
using Catmash.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catmash.Infrastructure.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly ApplicationDbContext _context;

        public VoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Vote vote)
        {
            await _context.Votes.AddAsync(vote);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Vote>> GetVotesByUserAsync(string voterId)
        {
            return await _context.Votes
                .Where(v => v.VoterId == voterId)
                .ToListAsync();
        }
    }

}
