using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catmash.Domain.Interfaces
{
    public interface IVoteRepository
    {
        Task AddAsync(Vote vote);
        Task<List<Vote>> GetVotesByUserAsync(string voterId);
    }
}
