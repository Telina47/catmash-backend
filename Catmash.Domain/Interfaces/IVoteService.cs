using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catmash.Domain.Interfaces
{
    public interface IVoteService
    {
        Task VoteForCatsAsync(string winnerId, string loserId, string voterId);
    }

}
