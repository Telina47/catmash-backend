using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catmash.Domain.Interfaces
{
    public interface IEloService
    {
        void ApplyElo(Cat winner, Cat loser);
    }
}
