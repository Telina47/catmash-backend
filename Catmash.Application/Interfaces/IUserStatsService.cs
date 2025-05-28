using Catmash.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catmash.Application.Interfaces
{
    public interface IUserStatsService
    {
        Task<UserVsGlobalStatsDto> GetComparisonAsync(string voterId, TopCount topCount);
    }
}
